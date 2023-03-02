using System.Data;
using API.Model;
using API.Validate;
using DataLibrary.Interface;
using DataLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IFoodData _foodData;
        private readonly IOrderData _orderData;

        public OrderController(IFoodData foodData, IOrderData orderData)
        {
            _foodData = foodData;
            _orderData = orderData;
        }

        [HttpPost]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(OrderModel order)
        {
            var food = await _foodData.GetFood();
            order.Total = order.Quantity * food.Single(x => x.Id == order.FoodId).Price;
            int id = await _orderData.CreateOrder(order);
            return Ok(new { Id = id });
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            if (id == 0)
                return BadRequest();
            var order = await _orderData.GetOrderById(id);
            if (order != null)
            {
                var food = await _foodData.GetFood();
                var output = new
                    { Order = order, ItemPurchased = food.FirstOrDefault(x => x.Id == order.FoodId)?.Title };

                return Ok(new { order });
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put([FromBody]OrderUpdateModel model)
        {
            await _orderData.UpdateOrder(model.Id, model.OrderName);
            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            await _orderData.DeleteOrder(id);
            return Ok();
        }
    }
}

