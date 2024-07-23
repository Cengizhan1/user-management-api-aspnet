using System.Diagnostics;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace UserWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly string _hostName;
        private readonly string _queueName;
        private readonly string _userName;
        private readonly string _password;
        private readonly string _virtualHost;
        private readonly int _port;

        public Worker(IConfiguration configuration)
        {
            var rabbitMqConfig = configuration.GetSection("RabbitMq");
            _hostName = rabbitMqConfig["HostName"];
            _queueName = rabbitMqConfig["QueueName"];
            _userName = rabbitMqConfig["UserName"];
            _password = rabbitMqConfig["Password"];
            _virtualHost = rabbitMqConfig["VirtualHost"];
            _port = int.Parse(rabbitMqConfig["Port"]);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory()
            {
                HostName = _hostName,
                UserName = _userName,
                Password = _password,
                VirtualHost = _virtualHost,
                Port = _port
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: _queueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    try
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine("Received message: {0}", message);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Error processing message: " + ex.Message);
                    }
                };

                channel.BasicConsume(queue: _queueName,
                                     autoAck: true,
                                     consumer: consumer);

                // Task.Delay ile bir bekleme süresi ekleyerek baðlantýnýn kesilmemesini saðlýyoruz
                return Task.Delay(Timeout.Infinite, stoppingToken);
            }
        }
    }
}