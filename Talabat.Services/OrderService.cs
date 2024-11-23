using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core;
using Talabat.core.Entities;
using Talabat.core.Entities.OrderAggregates;
using Talabat.core.Repostories.Contract;
using Talabat.core.Services.Contract;
using Talabat.core.Specifications.Oder_Specs;

namespace Talabat.Services
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderService : IOrderService
    {
       
        #region Constractor Region

        private readonly IBasketRepositoty _basketRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;

        ///private readonly IGenericRepostory<Product> _productRepo;
        ///private readonly IGenericRepostory<DeliveryMethod> _deliveryMethodRepo;
        ///private readonly IGenericRepostory<Order> _orderRepo;

        public OrderService(IBasketRepositoty basketRepo,
               IUnitOfWork unitOfWork ,
               IPaymentService paymentService
            ///IGenericRepostory<Product> productRepo ,
            ///IGenericRepostory<DeliveryMethod> DeliveryMethodRepo , 
            ///IGenericRepostory<Order> orderRepo

            )
        {
            _basketRepo = basketRepo;
            _unitOfWork = unitOfWork;
            this._paymentService = paymentService;
            ///this._productRepo = productRepo;
            ///_deliveryMethodRepo = DeliveryMethodRepo;
            ///this._orderRepo = orderRepo;
        }

        #endregion


        #region  Create Order Region

        public async Task<Order?> CreateOrderAsync(string buyerEmail, string BasketId, int deliveryMethodId, OrderAddress shippingAddress)
        {
            var orderRepo = _unitOfWork.Repostory<Order>();
            var basket = await _basketRepo.GetBasketAsync(BasketId);
            var OrderItems = new List<OrderItem>();

            if (basket?.Items?.Count > 0)
            {
                var productRepo = _unitOfWork.Repostory<Product>();
                foreach (var item in basket.Items)
                {
                    var product = await productRepo.GetByIdAsync(item.Id);

                    var productitemOrdered = new ProductItemOrderd(item.Id, product.Name, product.PictureUrl);
                    var orderitem = new OrderItem(productitemOrdered, product.Price, item.Quantity);
                    OrderItems.Add(orderitem);
                }

            }

            var subtotal = OrderItems.Sum(orderItemss => orderItemss.Price * orderItemss.Quantity);
            var DeliveryMethod = await _unitOfWork.Repostory<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

            var orderSpec = new OrderWithPaymentIntentIdSpecifications(basket.PaymentIntendId);
            var existingOrder = await orderRepo.GetEntityWithSpecAsync(orderSpec);
            if (existingOrder != null)
            {
                orderRepo.Delete(existingOrder);
                await _paymentService.CreateOrUpdatePaymentIntend(BasketId);
            }
            var order = new Order(buyerEmail, shippingAddress, DeliveryMethod, OrderItems, subtotal,basket.PaymentIntendId);
            await orderRepo.AddAsync(order);

            int result = await _unitOfWork.CompleteAsync();

            if (result <= 0)
                return null;
            return order;


        }


        #endregion


        #region Get Orders for User Region

        public async Task<IReadOnlyList<Order>> GetOrdersforUserAsync(string buyerEmail)
        {
            var orderRepo = _unitOfWork.Repostory<Order>();
            var spec = new OrderSpecifications(buyerEmail);
            var orders = await orderRepo.GetAllWithSpecAsync(spec);

            return orders;
        }
        #endregion

      
        #region Get Order By Id for User  Region

        public Task<Order?> GetOrderByIdforUserAsync(int orderId, string buyerEmail)
        {
            var orderRepo = _unitOfWork.Repostory<Order>();
            var Orderspec = new OrderSpecifications(orderId, buyerEmail);
            var orders = orderRepo.GetEntityWithSpecAsync(Orderspec);

            return orders;
        }

        #endregion

       
        #region Get Delivery Methods Async Region

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
         => await _unitOfWork.Repostory<DeliveryMethod>().GetAllAsync();

        ///  {  var deliveryMethodsRepo = _unitOfWork.Repostory<DeliveryMethod>();
        ///    var deliveryMethods  = deliveryMethodsRepo.GetAllAsync();
        ///    return deliveryMethods;}

        #endregion

    }
}
