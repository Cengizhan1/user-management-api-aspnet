using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Services;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace Service.Services
{
    public class RabbitMqService : IRabbitMqService
    {
        private readonly string _hostName;
        private readonly string _queueName;
        private readonly string _userName;
        private readonly string _password;
        private readonly string _virtualHost;
        private readonly int _port;

        public RabbitMqService(IConfiguration configuration)
        {
            var rabbitMqConfig = configuration.GetSection("RabbitMq");
            _hostName = rabbitMqConfig["HostName"];
            _queueName = rabbitMqConfig["QueueName"];
            _userName = rabbitMqConfig["UserName"];
            _password = rabbitMqConfig["Password"];
            _virtualHost = rabbitMqConfig["VirtualHost"];
            _port = int.Parse(rabbitMqConfig["Port"]);
        }

        public void SendMessage(string message)
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

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: _queueName,
                                     basicProperties: null,
                                     body: body);
            }
        }
    }
}