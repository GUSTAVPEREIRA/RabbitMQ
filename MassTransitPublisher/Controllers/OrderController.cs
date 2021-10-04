using System;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Shared.Model;
using Microsoft.AspNetCore.Mvc;

namespace MassTransitPublisher.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IBus _bus;

        public OrderController(IBus bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket(Ticket ticket)
        {
            if (ticket != null)
            {
                ticket.Booked = DateTime.Now;
                Uri uri = new Uri("rabbitmq://localhost/orderTicketQueue");
                var endPoint = await _bus.GetSendEndpoint(uri);
                await endPoint.Send(ticket);
                return Ok();
            }

            return BadRequest();
        }
    }
}