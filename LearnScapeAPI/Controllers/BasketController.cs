
using LearnScapeCore.BusinessModels;
using LearnScapeCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LearnScapeAPI.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepo _basketRepo;
        public BasketController(IBasketRepo basketRepo)
        {
            _basketRepo = basketRepo;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketById(string id)
        {
            var basket = await _basketRepo.GetBasketAsync(id);
            return Ok(basket ?? new CustomerBasket(id));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
        {
            var updatedBasket = await _basketRepo.UpdateBasketAsync(basket);
            return Ok(updatedBasket);
        }

        [HttpDelete]
        public async Task DeleteBasketAsync(string id)
        {
            await _basketRepo.DeleteBasketAsync(id);
        }

    }
}
