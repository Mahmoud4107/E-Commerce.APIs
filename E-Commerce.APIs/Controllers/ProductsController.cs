using AutoMapper;
using E_Commerce.APIs.Dtos;
using E_Commerce.APIs.Errors;
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
        private readonly IGenericRepository<ProductBrand> _brandRepo;
        private readonly IGenericRepository<ProductCategory> _categoryRepo;

        public ProductsController(IGenericRepository<Product> repository,IMapper mapper,
                                  IGenericRepository<ProductBrand> brandRepo,IGenericRepository<ProductCategory> categoryRepo)
        {
            _repository = repository;
            _mapper = mapper;
            _brandRepo = brandRepo;
            _categoryRepo = categoryRepo;
        }

        [HttpGet]
        // api/product
        public async Task<ActionResult<IEnumerable<ProductToReturn>>> GetAllProduct(string? sort,int? brandId,int? categoryId)
        {
            var productspec = new ProductSpecfication(sort,brandId,categoryId);
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
                return NotFound(new ApiResponse(404));

            var ReturnProduct = _mapper.Map<Product, ProductToReturn>(product);

            return Ok(ReturnProduct);
        }

        [HttpGet("brands")]

        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetBrands()
        {
            var brands = await _brandRepo.GetAllAsync();
            return Ok(brands);
        }

        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetCategories()
        {
            var categories = await _categoryRepo.GetAllAsync();
            return Ok(categories);
        }
    }
}
