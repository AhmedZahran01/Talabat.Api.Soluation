using Talabat.core.Entities.OrderAggregates;

namespace Talabat.Api.Soluation2a.DTOs
{
    public class OrderToReturnDto
    {
        #region Properties Region
       
        public int Id { get; set; }
        
        public string BuyerEmail { get; set; }
        
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        
        public string status { get; set; }

        public OrderAddress ShippingAddress { get; set; }

        public  string DeliveryMethodName { get; set; }
        public  decimal DeliveryMethodCost { get; set; }
        
        public ICollection<OrderItemDto> Items { get; set; } = new HashSet<OrderItemDto>();
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
         
        public string PaymentIntendId { get; set; } = string.Empty;

        #endregion

    }
}
