using E_Commerce.APIs.Errors;
using E_Commerce.Core.Entities;
using E_Commerce.Core.RepostriesContruct;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.APIs.Controllers
{
    public class BasketController : BaseAPIController
    {
        private readonly IBasketRepository basketRepo;

        public BasketController(IBasketRepository _basketRepo)
        {
            basketRepo = _basketRepo;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
        {
            var basket = await basketRepo.GetBasketAsync(id);

            if(basket is null)
            {
                return new CustomerBasket(id);
            }
            return Ok(basket);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
        {
            var CreatedOrUpdated = await basketRepo.UpdateBasketAsync(basket);

            if(CreatedOrUpdated is null)
            {
                return BadRequest(new ApiResponse(400));
            }
            return Ok(CreatedOrUpdated);
        }
        [HttpDelete]
        public async Task DeleteBasket(string id)
        {
             await basketRepo.DeleteBasketAsync(id);
        }
    }
}
