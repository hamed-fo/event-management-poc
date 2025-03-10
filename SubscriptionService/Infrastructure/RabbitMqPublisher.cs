using System.Text;
using RabbitMQ.Client;

namespace SubscriptionService.Infrastructure;

public class RabbitMqPublisher : IDisposable
{
    private string _queueName;
    private IConnection _connection;
    private IChannel _channel;

    public RabbitMqPublisher(IConfiguration configuration)
    {
        _queueName = configuration["SUBSCRIPTION_QUEUE"];
        var host = configuration["RABBITMQ_HOST"];
        if (string.IsNullOrWhiteSpace(host) || string.IsNullOrWhiteSpace(_queueName))
        {
            throw new Exception("RabbitMq configuration is missing");
        }

        var factory = new ConnectionFactory() { HostName = host };
        _connection = Task.Run(async () => await factory.CreateConnectionAsync()).Result;
        _channel = Task.Run(async () => await _connection.CreateChannelAsync()).Result;
    }
    
    public async Task PublishRegistrationAsync(string registrationJson)
    {
        var body = Encoding.UTF8.GetBytes(registrationJson);

        await _channel.BasicPublishAsync(
            exchange: "",
            routingKey: _queueName,
            body: body
        );
        
        Console.WriteLine($"Registration published successfully to {_queueName}");
    }

    public void Dispose()
    {
        _channel?.CloseAsync().Wait();
        _connection?.CloseAsync().Wait();
        _channel?.DisposeAsync().AsTask().Wait();
        _connection?.DisposeAsync().AsTask().Wait();
    }
}