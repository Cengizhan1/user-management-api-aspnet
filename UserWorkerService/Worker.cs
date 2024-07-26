using System.Diagnostics;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace UserWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly RabbitMqUserConsumer _rabbitMqConsumer;

        public Worker(IConfiguration configuration)
        {
            _rabbitMqConsumer = new RabbitMqUserConsumer(configuration);
            _rabbitMqConsumer.MessageReceived += OnMessageReceived;
        }

        private void OnMessageReceived(object sender, string message)
        {
            Console.WriteLine("Received message: {0}", message);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.Register(() => _rabbitMqConsumer.Close());
            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
    }
}