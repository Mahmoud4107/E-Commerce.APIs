using AutoMapper;
using E_Commerce.APIs.Dtos;
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
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        // api/product
        public async Task<ActionResult<IEnumerable<ProductToReturn>>> GetAllProduct()
        {
            var productspec = new ProductSpecfication();
            var products = await _repository.GetAllAsyncWithSpec(productspec);

            var ReturnProduct = _mapper.Map< IEnumerable<Product>,IEnumerable<ProductToReturn>>(products);

            return Ok(ReturnProduct);
        }
        [HttpGet("{id}")]
        //api/product/id
        public async Task<ActionResult<ProductToReturn>> GetProductById(int id)
        {
            var productspec = new ProductSpecfication(id);
            var product = await _repository.GetByIdAsyncWithSpec(productspec);

            if (product is null)
                return NotFound(new {Message = "Not Found", StatusCode = 404});

            var ReturnProduct = _mapper.Map<Product, ProductToReturn>(product);

            return Ok(ReturnProduct);
        }
    }
}
