using FastDelivery.Api.DTOs.Auth;
using FastDelivery.Api.DTOs.Client;
using FastDelivery.Api.DTOs.Order;
using FastDelivery.Api.Models;
using FastDelivery.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FastDelivery.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class OrderController(IOrdersService orderService) : ControllerBase
    {
        private readonly IOrdersService _orderService = orderService;
        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateOrderDto dto)
        {
            try
            {
                var response = await _orderService.CreateOrderAsync(dto);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpGet("driver/{id}")]
        public async Task<IActionResult> OrderByDriverId(Guid id)
        {
            var response = await _orderService.GetOrdersByDriverAsync(id);

            return Ok(response);
        }
        [Authorize]
        [HttpGet("driver/my-orders")]
        public async Task<IActionResult> MyOrders()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
                throw new Exception("Id del Usuario no encontrado");

            var userId = Guid.Parse(userIdClaim);

            var response = await _orderService.GetOrdersByDriverAsync(userId);

            return Ok(response);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> OrderById(int id)
        {
            var response = await _orderService.GetOrderById(id);

            return Ok(response);
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(UpdateOrderDto dto, int id)
        {
            try
            {
                var response = await _orderService.UpdateOrder(dto, id);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus( UpdateOrderStatusDto dto, int id)
        {
            try
            {
                var result = await _orderService.UpdateStatus(dto, id);

                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}/history")]
        public async Task<IActionResult> GetOrderHistory(int id)
        {
            var result = await _orderService.GetOrderHistory(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            try
            {
                var response = await _orderService.DeleteOrderAsync(id);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
    }
}
