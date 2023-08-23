using FundooSubscriber.Services;
using MassTransit;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;

namespace FundooSubscriber
{
    public class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                 .AddJsonFile("F:\\FundooNotesApplication\\FundooSubscriber\\appsettings.json")
                 .Build();

            var factory = new ConnectionFactory
            {
                HostName = configuration["RabbitMQSettings:HostName"],
                UserName = configuration["RabbitMQSettings:UserName"],
                Password = configuration["RabbitMQSettings:Password"]
            };

            var busControl = MassTransit.Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri(configuration["RabbitMQSettings:HostUri"]), h =>
                {
                    h.Username(configuration["RabbitMQSettings:UserName"]);
                    h.Password(configuration["RabbitMqSettings:Password"]);
                });

                cfg.ReceiveEndpoint("User-registratin-Queue", e =>
                {
                    e.Consumer<UserRegistrationEmailSubscriber>();
                });
            });

            var subscriber = new RabbitMQSubscriber(factory,configuration, busControl);
            subscriber.ConsumeMessages();

            Console.WriteLine("Press any key to exit..");
            Console.ReadKey();
        }
    }
}
