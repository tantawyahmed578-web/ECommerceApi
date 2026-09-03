using ECommerceApi.Application.DTOs.OrderItemDto;
using ECommerceApi.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ECommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        private int CurrentCustomerId { get { var customerIdClaim = User.FindFirstValue(JwtRegisteredClaimNames.Sub); 
                if (string.IsNullOrEmpty(customerIdClaim) || !int.TryParse(customerIdClaim, out var customerId)) 
                    throw new UnauthorizedAccessException("Unable to determine the current customer from the token."); 
                return customerId; } } // Get the current customer ID from the JWT claims

        [HttpPost]
        public async Task<ActionResult<OrderDto>> Create(CreateOrderDto dto)
        {
            var order = await _orderService.CreateOrderAsync(CurrentCustomerId, dto);
            return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
        }   

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetById(int id) // Endpoint to get a specific order by ID
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order is null) return NotFound();
            return Ok(order);
        }

        [HttpGet("my-orders")]
        public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetMyOrders() // Endpoint to get all orders for the current customer
        {
            var orders = await _orderService.GetByCustomerAsync(CurrentCustomerId);
            return Ok(orders);
        }

    }
}
