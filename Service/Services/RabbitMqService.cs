using System.Text;
using Core.Dtos;
using Core.Services;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Service.Services
{
    public class RabbitMqService : IRabbitMqService
    {
        private readonly RabbitMqInfo _rabbitMqInfo;

        public RabbitMqService(IOptions<RabbitMqInfo> rabbitMqInfo)
        {
            _rabbitMqInfo = rabbitMqInfo.Value;
        }

        public void SendMessage(string message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = _rabbitMqInfo.HostName,
                UserName = _rabbitMqInfo.UserName,
                Password = _rabbitMqInfo.Password,
                VirtualHost = _rabbitMqInfo.VirtualHost,
                Port = _rabbitMqInfo.Port
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: _rabbitMqInfo.QueueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: _rabbitMqInfo.QueueName,
                                     basicProperties: null,
                                     body: body);
            }
        }
    }
}