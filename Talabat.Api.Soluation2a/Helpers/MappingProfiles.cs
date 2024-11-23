using AutoMapper;
using Talabat.Api.Soluation2a.DTOs;
using Talabat.core.Entities;
using Talabat.core.Entities.Identity;
using Talabat.core.Entities.OrderAggregates;

namespace Talabat.Api.Soluation2a.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.Brand       , o => o.MapFrom(s => s.Brand.Name)         )
                .ForMember(d => d.category    , o => o.MapFrom(s => s.category.Name)      )
                .ForMember(d => d.PictureUrl  , o => o.MapFrom<ProductPictureUrlResolver> () );

            CreateMap<CustomerBasketDto, CustomerBasket>();
          
            CreateMap<BasketItemDto, BasketItem>();
            
            CreateMap<OrderAddressDto, OrderAddress>();
            CreateMap<Address, OrderAddressDto>().ReverseMap();

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethodName, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.DeliveryMethodCost, o => o.MapFrom(s => s.DeliveryMethod.Cost));
           
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.productName, o => o.MapFrom(s => s.product.productName))
                .ForMember(d => d.productId, o => o.MapFrom(s => s.product.productId))
                .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.product.PictureUrl))
                .ForMember(d => d.PictureUrl , o => o.MapFrom<OrderItemPictureUrlResolver>() );

        }
    }
}
