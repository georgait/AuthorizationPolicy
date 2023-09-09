using PaymentGateway.Infrastructure.Data.Events;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace PaymentGateway.Infrastructure.Messaging;

public class RabbitMQPaymentConfirmationPublisher : DefaultRabbitMQMessagePublisher<PaymentConfirmedEvent>
{
    private readonly ILogger<RabbitMQPaymentConfirmationPublisher> _logger;

    public RabbitMQPaymentConfirmationPublisher(
        IConfiguration configuration,
        ILogger<RabbitMQPaymentConfirmationPublisher> logger) : base(configuration, logger)
    {
        _logger = logger;
    }

    public override void Publish(PaymentConfirmedEvent @event)
    {
        _logger.LogInformation("Publishing payment confirmation event to RabbitMQ...");

        var message = JsonSerializer.Serialize(@event);

        if (Connection is not null && Connection.IsOpen)
        {
            _logger.LogInformation("Connection is open, publishing message...");
            SendMessage(message);
        }
        else
        {
            _logger.LogInformation("Connection is closed, opening connection and publishing message...");
        }
    }

    private void SendMessage(string message)
    {
        var body = Encoding.UTF8.GetBytes(message);
        
        Channel.BasicPublish(
            exchange: "paymentgateway",
            routingKey: "",
            basicProperties: null,
            body: body);

        _logger.LogInformation("Message published");
    }    
}
