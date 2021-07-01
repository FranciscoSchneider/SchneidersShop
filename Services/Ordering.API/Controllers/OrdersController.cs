using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.CreateOrder;
using Ordering.Application.InvoiceOrder;
using System.Threading.Tasks;

namespace Ordering.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController  : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderCommand command)
        {
            if (await _mediator.Send(command) is var result && result.IsError)
                return BadRequest(result.Message);
            return Ok(result.Data);
        }

        [HttpPut("{id}/Invoice")]
        public async Task<IActionResult> Invoice(int id)
        {
            var command = new InvoiceOrderCommand { OrderId = id };
            if (await _mediator.Send(command) is var result && result.IsError)
                return BadRequest(result.Message);
            return Ok(result.Data);
        }
    }
}
