using AutoMapper;
using Core.Entities;

namespace WebApi.Dtos
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Monograph, MonographDto>()
                .ForMember(p => p.CategoryName, x => x.MapFrom(c => c.Category.Name))
                .ForMember(p => p.ProductName, x => x.MapFrom(p => p.Product.Name));               
        }
    }
}
