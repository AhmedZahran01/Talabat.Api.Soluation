using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Talabat.Api.Soluation2a.Errors;
using Talabat.Api.Soluation2a.Helpers;
using Talabat.core;
using Talabat.core.Repostories.Contract;
using Talabat.core.Services.Contract;
using Talabat.Repository;
using Talabat.Services;

namespace Talabat.Api.Soluation2a.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public  static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddScoped(typeof(IPaymentService), typeof(PaymentService));
            services.AddScoped(typeof(IOrderService), typeof(OrderService));
            services.AddScoped(typeof(IProductService), typeof(ProductService));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

            #region MyRegion

            //webApplicationBuilder.Services.AddScoped<IGenericRepostory<Product>, GenericRepostory<Product>>();
            //webApplicationBuilder.Services.AddScoped<IGenericRepostory<ProductBrand>, GenericRepostory<ProductBrand>>();
            //webApplicationBuilder.Services.AddScoped<IGenericRepostory<ProductCategory>, GenericRepostory<ProductCategory>>();

            #endregion

            //services.AddScoped(typeof(IGenericRepostory<>), typeof(GenericRepostory<>));

            #region Comment For Db Radis
            services.AddScoped(typeof(IBasketRepositoty), typeof(BasketRepository));
            #endregion

            //webApplicationBuilder.Services.AddAutoMapper(m => m.AddProfile(new MappingProfiles()));
            services.AddAutoMapper(typeof(MappingProfiles));


            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (Actioncontext) =>
                {
                    var errorss = Actioncontext.ModelState.Where(p => p.Value.Errors.Count() > 0).
                                          SelectMany(p => p.Value.Errors).Select(E => E.ErrorMessage).ToArray();

                    var validationErrorResponse = new ApiValidationErrorRespone()
                    {
                        Errors = errorss

                    };
                    return new BadRequestObjectResult(validationErrorResponse);

                };
            });

            return services;
        }
    }
}
