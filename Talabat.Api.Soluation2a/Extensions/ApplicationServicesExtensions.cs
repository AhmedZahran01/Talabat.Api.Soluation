using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Talabat.Api.Soluation2a.Errors;
using Talabat.Api.Soluation2a.Helpers;
using Talabat.core.Repostories.Contract;
using Talabat.Repository;

namespace Talabat.Api.Soluation2a.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public  static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {

            #region MyRegion

            //webApplicationBuilder.Services.AddScoped<IGenericRepostory<Product>, GenericRepostory<Product>>();
            //webApplicationBuilder.Services.AddScoped<IGenericRepostory<ProductBrand>, GenericRepostory<ProductBrand>>();
            //webApplicationBuilder.Services.AddScoped<IGenericRepostory<ProductCategory>, GenericRepostory<ProductCategory>>();

            #endregion

            Services.AddScoped(typeof(IGenericRepostory<>), typeof(GenericRepostory<>));

            //webApplicationBuilder.Services.AddAutoMapper(m => m.AddProfile(new MappingProfiles()));
            Services.AddAutoMapper(typeof(MappingProfiles));


            Services.Configure<ApiBehaviorOptions>(options =>
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

            return Services;
        }
    }
}
