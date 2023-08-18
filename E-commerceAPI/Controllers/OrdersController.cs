using E_commerceAPI.Models;
using E_commerceAPI.Repository.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace E_commerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private IOrder _order;

        public OrdersController(IOrder order)
        {
            _order = order;
        }

        [HttpGet("All")]
        [Authorize(Roles = "Admin")]
        [ResponseCache(Duration = 120)]
        public ActionResult<IEnumerable<Order>> Index()
        {
            return Ok(_order.GetAll());
        }

        [HttpGet("Users/{id}")]
        [Authorize(Roles = "User, Admin")]
        [ResponseCache(Duration = 120)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public ActionResult<IEnumerable<Order>> GetOrderForUser([FromRoute] int? id)
        {
            if (id is null)
                return BadRequest("Required user id");

            return Ok(_order.GetForUser((int)id));
        }


        [HttpPost("Add")]
        [Authorize(Roles = "User, Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Order>> AddOrder([FromBody] OrderDto orderDto)
        {
            if(orderDto is null)
                return BadRequest("empty order error");

            Order order = new Order {
                UserId = orderDto.UserId,
                ProductId = orderDto.ProductId,
                Username = orderDto.Username,
                OrderDate = orderDto.OrderDate
            };
            await _order.Add(order);

            return CreatedAtAction(nameof(GetOrderForUser), new {id = order.UserId }, order);
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Roles = "User, Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public ActionResult Delete(int? id)
        {
            if (id is null)
                return BadRequest("Required order id");
            _order.Delete((int)id);
            return Ok();
        }




    }
}
