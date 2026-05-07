using MediatR;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Features.OrderItems.Commands.DeleteOrderItem;
using RestaurantManagement.Application.Features.OrderItems.Commands.UpdateOrderItem;

namespace RestaurantManagement.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrderItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrderItem([FromBody] UpdateOrderItemCommand command,
           CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result.Value);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOrderItem([FromBody] DeleteOrderItemCommand command,
           CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result.Value);
        }
    }
}
