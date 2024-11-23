using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities.OrderAggregates;

namespace Talabat.core.Specifications.Oder_Specs
{
    public class OrderWithPaymentIntentIdSpecifications : BaseSpecifications<Order>
    {
        public OrderWithPaymentIntentIdSpecifications(string PaymentIntentId) :
                               base(o => o.PaymentIntendId == PaymentIntentId )
        {
            
        }
    }
}
