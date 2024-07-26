using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace UserWorkerService
{
    public class RabbitMqUserConsumer
    {
        private readonly string _hostName;
        private readonly string _queueName;
        private readonly string _userName;
        private readonly string _password;
        private readonly string _virtualHost;
        private readonly int _port;
        private IConnection _connection;
        private IModel _channel;

        public event EventHandler<string> MessageReceived;

        public RabbitMqUserConsumer(IConfiguration configuration)
        {
            var rabbitMqConfig = configuration.GetSection("RabbitMq");
            _hostName = rabbitMqConfig["HostName"];
            _queueName = rabbitMqConfig["QueueName"];
            _userName = rabbitMqConfig["UserName"];
            _password = rabbitMqConfig["Password"];
            _virtualHost = rabbitMqConfig["VirtualHost"];
            _port = int.Parse(rabbitMqConfig["Port"]);
            InitializeRabbitMq();
        }

        private void InitializeRabbitMq()
        {
            var factory = new ConnectionFactory()
            {
                HostName = _hostName,
                UserName = _userName,
                Password = _password,
                VirtualHost = _virtualHost,
                Port = _port
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: _queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                MessageReceived?.Invoke(this, message);
            };

            _channel.BasicConsume(queue: _queueName,
                                 autoAck: true,
                                 consumer: consumer);
        }

        public void Close()
        {
            _channel?.Close();
            _connection?.Close();
        }
    }
}
