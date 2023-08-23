using FundooSubscriber.Interface;
using FundooSubscriber.Models;
using MassTransit;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooSubscriber.Services
{
    public class RabbitMQSubscriber:IRabbitMQSubscriber
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly IConfiguration configuration;
        private readonly IBusControl busControl;

        public RabbitMQSubscriber(ConnectionFactory connectionFactory, IConfiguration configuration, IBusControl busControl)
        {
            _connectionFactory = connectionFactory;
            this.configuration = configuration;
            this.busControl = busControl;

            ConsumeMessages();
        }
        public void ConsumeMessages()
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                Console.WriteLine("Connection to RabbitMQ Server established");

                using(var channel = connection.CreateModel())
                {
                    var queuename = "User-Registration-Queue";
                    channel.QueueDeclare(queue: queuename, durable: false, exclusive: false, autoDelete: false, arguments: null);

                    var Consumer = new EventingBasicConsumer(channel);
                    Consumer.Received += async (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);

                        await busControl.Publish<UserRegistrationMesssage>(new
                        {
                            Email = message
                        });
                    };

                    channel.BasicConsume(queue:queuename,autoAck:true,consumer:Consumer);
                }
            }
        }
    }
}
