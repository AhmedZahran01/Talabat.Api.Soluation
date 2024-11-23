using Microsoft.Extensions.Configuration;
using Stripe;
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
using Product = Talabat.core.Entities.Product;

namespace Talabat.Services
{
    public class PaymentService : IPaymentService
    {
        #region Constractor Region

        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketRepositoty _basketRepositoty;
        private readonly IConfiguration _configuration;

        public PaymentService(IUnitOfWork unitOfWork,
                    IBasketRepositoty basketRepositoty, IConfiguration configuration)
        {
            this._unitOfWork = unitOfWork;
            this._basketRepositoty = basketRepositoty;
            this._configuration = configuration;
        }

        #endregion

         
        #region Create Or Update Payment Inte nd Region

        public async Task<CustomerBasket> CreateOrUpdatePaymentIntend(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];
            var basket = await _basketRepositoty.GetBasketAsync(basketId);
            if (basket == null) return null;
            var shippingPrice = 0m;
            //basket.DeliveryMenthodId = 1;
            if (basket.DeliveryMenthodId.HasValue)
            {
                var deliveryMethod = await _unitOfWork.Repostory<DeliveryMethod>().GetByIdAsync(basket.DeliveryMenthodId.Value);
                basket.ShippingPrice = deliveryMethod.Cost;
                shippingPrice = deliveryMethod.Cost;
            }
            if (basket?.Items.Count > 0)
            {
                foreach (var item in basket.Items)
                {
                    var product = await _unitOfWork.Repostory<Product>().GetByIdAsync(item.Id);
                    if (item.Price != product.Price)
                        item.Price = product.Price;

                }
            }

            PaymentIntent paymentIntend;
            
            #region Create Payment Intend Region

            PaymentIntentService paymentIntentService = new PaymentIntentService();
            if (string.IsNullOrEmpty(basket.PaymentIntendId))
            {
                //var options = PaymentIntendCreateOptions()
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)basket.Items.Sum(item => item.Price * 100 * item.Quantity) + (long)shippingPrice * 100,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string>() { "card"}
                };
                paymentIntend =  await paymentIntentService.CreateAsync(options);
                basket.PaymentIntendId = paymentIntend.Id;
                basket.ClientSecret = paymentIntend.ClientSecret;

            }

            #endregion
          
            #region Update Payment Intend Region
            //change amount 
            else
            {
                var updateOptions = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)basket.Items.Sum(item => item.Price * 100 * item.Quantity) + (long)shippingPrice * 100,
                };

                await paymentIntentService.UpdateAsync(basket.PaymentIntendId, updateOptions);
            }
            #endregion
    
            await _basketRepositoty.UpdateBasketAsync(basket);
            return basket;
        }

        #endregion

    }
}
