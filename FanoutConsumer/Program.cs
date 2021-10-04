using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace FanoutConsumer
{
    class Program
    {
        private static IModel channel;
        
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                VirtualHost = "/",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };

            using var conn = factory.CreateConnection();
            channel = conn.CreateModel();

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += Consumer_Received;
            var consumerTag = channel.BasicConsume("my.queue1", true, consumer);
            Console.WriteLine($"{consumerTag}... Waiting for messages.");
            Console.WriteLine("Press any key to exit!");
            Console.ReadKey();
        }

        private static void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            var body = e.Body.Span;
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine(message);
            
            channel.BasicNack(e.DeliveryTag, false, false);
        }
    }
}