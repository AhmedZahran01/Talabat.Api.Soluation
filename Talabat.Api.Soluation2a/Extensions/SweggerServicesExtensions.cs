using Microsoft.AspNetCore.Builder;

namespace Talabat.Api.Soluation2a.Extensions
{
    public static class SweggerServicesExtensions
    {
        public static IServiceCollection AddSweggerServices(this IServiceCollection Services)
        { 
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen();

            return Services;
        }

        public static WebApplication UseSweggerAppMiddleWarw(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            //app.UseDeveloperExceptionPage();
            return app;
        }
    }
}
