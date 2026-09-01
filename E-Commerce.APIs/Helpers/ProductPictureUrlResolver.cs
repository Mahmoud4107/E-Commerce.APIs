using AutoMapper;
using E_Commerce.APIs.Dtos;
using E_Commerce.Core.Entities;

namespace E_Commerce.APIs.Helpers
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductToReturn, string>
    {
        private readonly IConfiguration _configuration;

        public ProductPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductToReturn destination, string destMember, ResolutionContext context)
        {
            return $"{_configuration["BaseUrl:LocalHost"]}{source.PictureUrl}";
        }
    }
}
