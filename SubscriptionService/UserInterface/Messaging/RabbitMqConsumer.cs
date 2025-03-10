using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SubscriptionService.Application;
using SubscriptionService.UserInterface.Dto;

namespace SubscriptionService.UserInterface.Messaging;

public class RabbitMqConsumer: IDisposable
{
    private string _queueName;
    private IConnection _connection;
    private IChannel _channel;
    private IServiceScopeFactory _serviceScopeFactory;

    public RabbitMqConsumer(IConfiguration configuration, IServiceScopeFactory serviceScopeFactory)
    {
        _queueName = configuration["EVENT_QUEUE"];
        var host = configuration["RABBITMQ_HOST"];
        if (string.IsNullOrWhiteSpace(host) || string.IsNullOrWhiteSpace(_queueName))
        {
            throw new Exception("RabbitMq configuration is missing");
        }
        
        var factory = new ConnectionFactory() { HostName = host };
        _connection = Task.Run(async () => await factory.CreateConnectionAsync()).Result;
        _channel = Task.Run(async () => await _connection.CreateChannelAsync()).Result;
        
        _serviceScopeFactory = serviceScopeFactory;
    }
    
    public async Task StartListeningAsync()
    {
        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.ReceivedAsync += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            try
            {
                var newEvent = JsonSerializer.Deserialize<NewEventDto>(message);

                if (newEvent != null)
                {
                    using var scope = _serviceScopeFactory.CreateScope();
                    var useCase = scope.ServiceProvider.GetRequiredService<EventUseCase>();
                    
                    await useCase.SaveEventAsync(newEvent);
                    Console.WriteLine($"Received and saved event: {message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing message: {ex.Message}");
            }
        };

        await _channel.BasicConsumeAsync(queue: _queueName, autoAck: true, consumer: consumer);

        Console.WriteLine($"Listening for messages on {_queueName}");
    }
    
    public void Dispose()
    {
        _channel?.CloseAsync().Wait();
        _connection?.CloseAsync().Wait();
        _channel?.DisposeAsync().AsTask().Wait();
        _connection?.DisposeAsync().AsTask().Wait();
    }
}