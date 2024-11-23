using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.core.Entities.OrderAggregates
{
    public class OrderItem:BaseEntity
    {
        #region Constractor Region

        public OrderItem()
        {

        }
        public OrderItem(ProductItemOrderd product, decimal price, int quantity)
        {
            this.product = product;
            Price = price;
            Quantity = quantity;
        }

        #endregion
     
        public ProductItemOrderd product { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
         

    }
}
