using AutoMapper;
using AutoMapper.Execution;
using Talabat.Api.Soluation2a.DTOs;
using Talabat.core.Entities;
using Talabat.core.Entities.OrderAggregates;

namespace Talabat.Api.Soluation2a.Helpers
{
    public class OrderItemPictureUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration _configuration;

        public OrderItemPictureUrlResolver(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
         
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {

            if (!string.IsNullOrEmpty(source.product.PictureUrl))
            {
                return $"{_configuration["ApiBaseUrl"]}/{source.product.PictureUrl}";
            }
            return string.Empty;
        }
    }
}
