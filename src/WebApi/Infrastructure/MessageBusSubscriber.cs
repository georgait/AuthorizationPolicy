using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace WebApi.Infrastructure;

public sealed class MessageBusSubscriber : BackgroundService
{
    private readonly IConfiguration _configuration;
    private readonly IEventProcessor _eventProcessor;
    private readonly ILogger<MessageBusSubscriber> _logger;
    private IConnection _connection;
    private IModel _channel;
    private string _queueName;

    public MessageBusSubscriber(
        IConfiguration configuration,
        IEventProcessor eventProcessor,
        ILogger<MessageBusSubscriber> logger)
    {
        _configuration = configuration;
        _eventProcessor = eventProcessor;
        _logger = logger;

        InitializeRabbitMQ();
    }

    private void InitializeRabbitMQ()
    {
        var factory = new ConnectionFactory
        {
            HostName = _configuration["RabbitMQ:Host"],
            Port = int.Parse(_configuration["RabbitMQ:Port"]!)
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.ExchangeDeclare(exchange: "paymentgateway", type: ExchangeType.Fanout);
        _queueName = _channel.QueueDeclare().QueueName;
        _channel.QueueBind(
            queue: _queueName,
            exchange: "paymentgateway",
            routingKey: "");

        _logger.LogInformation("Listening on the Message Bus...");

        _connection.ConnectionShutdown += OnConnectionShutdown;
    }

    private void OnConnectionShutdown(object? sender, ShutdownEventArgs e)
    {
        _logger.LogInformation("Connection was shut down");
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (sender, e) =>
        {
            _logger.LogInformation("Message received");
            var content = Encoding.UTF8.GetString(e.Body.ToArray());
            await _eventProcessor.ProcessAsync(content);
        };

        _channel.BasicConsume(
            queue: _queueName,
            autoAck: true,
            consumer: consumer);

        return Task.CompletedTask;
    }

    public override void Dispose()
    {
        if (_channel.IsOpen)
        {
            _channel.Close();
            _connection.Close();
        }
        base.Dispose();
    }
}
