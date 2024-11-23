using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities.OrderAggregates;

namespace Talabat.core.Services.Contract
{
    public interface IOrderService
    {
        Task<Order?> CreateOrderAsync(string buyerEmail, string BasketId, int deliveryMethodId,
                                            OrderAddress shippingAddress); 

        Task<IReadOnlyList<Order>> GetOrdersforUserAsync(string buyerEmail);

        Task<Order?> GetOrderByIdforUserAsync(int OrderId, string buyerEmail);

        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();

    }
}
