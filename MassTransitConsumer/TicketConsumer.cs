using System;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Shared.Model;
using Microsoft.Extensions.Logging;

namespace MassTransitProducer
{
    public class TicketConsumer : IConsumer<Ticket>
    {
        private readonly ILogger<TicketConsumer> _logger;

        public TicketConsumer(ILogger<TicketConsumer> logger)
        {
            _logger = logger;
        }
        
        public async Task Consume(ConsumeContext<Ticket> context)
        {
            await Console.Out.WriteLineAsync(context.Message.UserName);
            _logger.LogInformation($"Nova mensagem recebida: {context.Message.UserName} {context.Message.Location}");
        }
    }
}