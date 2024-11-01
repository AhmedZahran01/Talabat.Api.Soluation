using AutoMapper;
using Talabat.Api.Soluation2a.DTOs;
using Talabat.core.Entities;

namespace Talabat.Api.Soluation2a.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.Brand , o => o.MapFrom(s => s.Brand.Name))
                .ForMember(d => d.category , o => o.MapFrom(s => s.category.Name) )
                ;


        }
    }
}
