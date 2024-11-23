
using System.ComponentModel.DataAnnotations;
using Talabat.core.Entities.OrderAggregates;

namespace Talabat.Api.Soluation2a.DTOs
{
    public class OrderDto
    { 
        [Required]
        public string BasketId { get; set; }
        [Required]
        public int    DeliveryMethodId { get; set; }
        public OrderAddressDto ShippingAddress { get; set; }

    }
}
