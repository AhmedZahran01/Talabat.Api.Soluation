using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.core.Entities.OrderAggregates
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Pending")]
        pending,

        [EnumMember(Value = "Payment Recieved")]
        paymentRecieved,

        [EnumMember(Value = "Payment Failed")]
        paymentFailed

    }
}
