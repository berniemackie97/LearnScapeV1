
using AutoMapper;
using LearnScapeAPI.DTO;
using LearnScapeCore.BusinessModels;
using LearnScapeCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LearnScapeAPI.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepo _basketRepo;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepo basketRepo, IMapper mapper)
        {
            _basketRepo = basketRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketById(string id)
        {
            CustomerBasket basket = await _basketRepo.GetBasketAsync(id);
            return Ok(basket ?? new CustomerBasket(id));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDTO basket)
        {
            CustomerBasket customerBasket = _mapper.Map<CustomerBasketDTO, CustomerBasket>(basket);

            CustomerBasket updatedBasket = await _basketRepo.UpdateBasketAsync(customerBasket);
            
            return Ok(updatedBasket);
        }

        [HttpDelete]
        public async Task DeleteBasketAsync(string id)
        {
            await _basketRepo.DeleteBasketAsync(id);
        }

    }
}
