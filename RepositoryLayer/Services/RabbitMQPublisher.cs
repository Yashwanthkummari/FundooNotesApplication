using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services
{
    public class RabbitMQPublisher
    {
        private readonly ConnectionFactory _connectionFactory;

        public RabbitMQPublisher(ConnectionFactory Factory)
        {
            _connectionFactory = Factory;
        }

        public void PublishMessage(string queuename, string message)
        {
            using (var connection = _connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queuename, durable: false, exclusive: false, autoDelete: false, arguments: null);

                var messageBytes = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "", routingKey: queuename, basicProperties: null, body: messageBytes);
            }
        }
    }
}
