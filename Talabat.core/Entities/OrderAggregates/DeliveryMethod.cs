using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.core.Entities.OrderAggregates
{
    public class DeliveryMethod:BaseEntity
    {
        #region Constractor Region

        public DeliveryMethod()
        {

        }
        public DeliveryMethod(string shortName, string discription, decimal cost, string deliveryTime)
        {
            ShortName = shortName;
            Description = discription;
            Cost = cost;
            DeliveryTime = deliveryTime;
        }

        #endregion
     
        public string ShortName { get; set; }
        public string Description { get; set; }
        public string DeliveryTime { get; set; }
        public decimal Cost { get; set; } = 0;

    }
}
