using System.ComponentModel.DataAnnotations;
using Talabat.core.Entities;

namespace Talabat.Api.Soluation2a.DTOs
{
    public class CustomerBasketDto
    {

        [Required]
        public string Id { get; set; }

        public List<BasketItemDto> Items { get; set; }

        public int? DeliveryMenthodId { get; set; }
        public decimal ShippingPrice { get; set; }
        public string? PaymentIntendId { get; set; }
        public string? ClientSecret { get; set; }

    }
}
