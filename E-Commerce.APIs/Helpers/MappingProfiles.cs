using AutoMapper;
using E_Commerce.APIs.Dtos;
using E_Commerce.Core.Entities;

namespace E_Commerce.APIs.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturn>()
           .ForMember(des => des.Brand, option => option.MapFrom(S => S.Brand.Name))
           .ForMember(des => des.Category, option => option.MapFrom(S => S.Category.Name))
           //.ForMember(des => des.PictureUrl, option => option.MapFrom(S => $"https://localhost:7097/{S.PictureUrl}"));
           .ForMember(des => des.PictureUrl, option => option.MapFrom<ProductPictureUrlResolver>());

            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();
        }
    }
}
