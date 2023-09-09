using PaymentGateway.Infrastructure.Data.Base;
using PaymentGateway.Infrastructure.Messaging.Interfaces;
using RabbitMQ.Client;

namespace PaymentGateway.Infrastructure.Messaging;

public abstract class DefaultRabbitMQMessagePublisher<TEvent>: IMessagePublisher<TEvent> where TEvent : Event
{
    protected IConfiguration Configuration { get; }
    protected IConnection? Connection { get; set; }
    protected IModel? Channel { get; set; }

    private readonly ILogger _logger;

    protected DefaultRabbitMQMessagePublisher(
        IConfiguration configuration, 
        ILogger logger)
    {
        Configuration = configuration;
        _logger = logger;
        var factory = new ConnectionFactory
        {
            HostName = Configuration["RabbitMQ:Host"],
            Port = int.Parse(Configuration["RabbitMQ:Port"]!)
        };

        try
        {
            Connection = factory.CreateConnection();
            Channel = Connection.CreateModel();
            Channel.ExchangeDeclare(exchange: "paymentgateway", type: ExchangeType.Fanout);
            Connection.ConnectionShutdown += OnConnectionShutdown;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "RabbitMQ connection error");
        }
    }

    protected void OnConnectionShutdown(object? sender, ShutdownEventArgs e)
    {
        _logger.LogInformation("RabbitMQ connection shutdown");
    }

    public abstract void Publish(TEvent @event);

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing && Channel is not null && Channel.IsOpen)
        {
            _logger.LogInformation("Disposing RabbitMQ connection");
            Channel.Close();
            Connection?.Close();
        }
    }
}
