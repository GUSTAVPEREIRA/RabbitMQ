using System;
using System.ComponentModel;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Demo
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
            channel.ExchangeDeclare("ex.direct", "direct", true, false, null);

            channel.BasicPublish("ex.direct", "info", null, Encoding.UTF8.GetBytes("Message with routing key info"));
            channel.BasicPublish("ex.direct", "warning", null,
                Encoding.UTF8.GetBytes("Message with routing key warning"));
            channel.BasicPublish("ex.direct", "error", null, Encoding.UTF8.GetBytes("Message with routing key error"));

            channel.QueueDeclare("my.infos", true, false, false);
            channel.QueueDeclare("my.errors", true, false, false);
            channel.QueueDeclare("my.warnings", true, false, false);


            channel.BasicPublish("ex.direct", "info", null, Encoding.UTF8.GetBytes("Message with routing key info"));
            channel.BasicPublish("ex.direct", "warning", null,
                Encoding.UTF8.GetBytes("Message with routing key warning"));
            channel.BasicPublish("ex.direct", "error", null, Encoding.UTF8.GetBytes("Message with routing key error"));
        }
    }
}