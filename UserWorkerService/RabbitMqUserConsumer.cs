using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace UserWorkerService
{
    public class RabbitMqUserConsumer
    {
        private readonly RabbitMqInfo _rabbitMqInfo;
        private IConnection _connection;
        private IModel _channel;

        public event EventHandler<string> MessageReceived;

        public RabbitMqUserConsumer(IOptions<RabbitMqInfo> rabbitMqInfo)
        {
            _rabbitMqInfo = rabbitMqInfo.Value;
            InitializeRabbitMq();
        }

        private void InitializeRabbitMq()
        {
            var factory = new ConnectionFactory()
            {
                HostName = _rabbitMqInfo.HostName,
                UserName = _rabbitMqInfo.UserName,
                Password = _rabbitMqInfo.Password,
                VirtualHost = _rabbitMqInfo.VirtualHost,
                Port = _rabbitMqInfo.Port
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: _rabbitMqInfo.QueueName,
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

            _channel.BasicConsume(queue: _rabbitMqInfo.QueueName,
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
