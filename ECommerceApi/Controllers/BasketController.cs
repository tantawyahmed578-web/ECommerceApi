using AutoMapper;
using ECommerceApi.Application.DTOs.Basket;
using ECommerceApi.Domain.Entities;
using ECommerceApi.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerBasketDto>> GetBasketById(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);
            return Ok(_mapper.Map<CustomerBasketDto>(basket ?? new CustomerBasket(id)));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> UpdateBasket(CustomerBasketDto basketDto)
        {
            var customerBasket = _mapper.Map<CustomerBasket>(basketDto);
            var updatedBasket = await _basketRepository.UpdateBasketAsync(customerBasket);
            if (updatedBasket == null) return BadRequest("Problem updating the basket.");
            return Ok(_mapper.Map<CustomerBasketDto>(updatedBasket));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBasket(string id)
        {
            await _basketRepository.DeleteBasketAsync(id);
            return NoContent();
        }
    }
}
