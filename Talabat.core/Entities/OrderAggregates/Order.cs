using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.core.Entities.OrderAggregates
{
    public class Order : BaseEntity
    { 
        #region Constractor Region

        public Order()
        {

        }
        public Order(string buyerEmail, OrderAddress shippingAddress, DeliveryMethod? deliveryMethod,
                                 ICollection<OrderItem> items, decimal subTotal, string paymentIntendId)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            this.Items = items;
            SubTotal = subTotal;
            PaymentIntendId = paymentIntendId;

        }
        #endregion

        #region Properties Region

        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public OrderStatus status { get; set; } = OrderStatus.pending;
        public OrderAddress ShippingAddress { get; set; }
        public int? DeliveryMethodId { get; set; }
        public DeliveryMethod? DeliveryMethod { get; set; } = new DeliveryMethod();
        public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();
        public decimal SubTotal { get; set; }

        //[NotMapped]
        //public decimal Total { get { return SubTotal + DeliveryMethod.Cost; } }  
        //public decimal Total => SubTotal + DeliveryMethod.Cost;  

        public decimal GetTotal() => SubTotal + DeliveryMethod.Cost;
        public string PaymentIntendId { get; set; }

        #endregion

    }

}
