using AutoMapper;
using E_Commerce.APIs.Dtos;
using E_Commerce.Core.Entities;

namespace E_Commerce.APIs.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
             CreateMap<Product,ProductToReturn>()
            .ForMember(des => des.Brand, option => option.MapFrom(S => S.Brand.Name))
            .ForMember(des => des.Category,option => option.MapFrom(S => S.Category.Name));
        }
    }
}
