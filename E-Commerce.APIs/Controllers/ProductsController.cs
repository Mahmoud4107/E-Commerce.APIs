using E_Commerce.Core.Entities;
using E_Commerce.Core.RepostriesContruct;
using E_Commerce.Core.Specification;
using E_Commerce.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.APIs.Controllers
{
    public class ProductsController : BaseAPIController
    {
        private readonly IGenericRepository<Product> _repository;

        public ProductsController(IGenericRepository<Product> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        // api/product
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProduct()
        {
            var productspec = new ProductSpecfication();
            var products = await _repository.GetAllAsyncWithSpec(productspec);

            return Ok(products);
        }

        [HttpGet("{id}")]
        //api/product/id
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var productspec = new ProductSpecfication(id);
            var product = await _repository.GetByIdAsyncWithSpec(id, productspec);

            if (product is null)
                return NotFound(new {Message = "Not Found", StatusCode = 404});

            return Ok(product);
        }

    }
}
