using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;
using StackExchange.Redis;
using Talabat.Api.Soluation2a.Errors;
using Talabat.Api.Soluation2a.Extensions;
using Talabat.Api.Soluation2a.Helpers;
using Talabat.Api.Soluation2a.MiddleWares;
using Talabat.core.Entities;
using Talabat.core.Repostories.Contract;
using Talabat.Repository;
using Talabat.Repository.Data;

namespace Talabat.Api.Soluation2a
{
    public class Program
    {

        public static async Task  Main(string[] args)
        {
            
            var webApplicationBuilder = WebApplication.CreateBuilder(args);
          
          #region Configure Services

            // Add services to the container.

            webApplicationBuilder.Services.AddControllers();

            webApplicationBuilder.Services.AddSweggerServices();

            webApplicationBuilder.Services.AddDbContext<StoreContext>(options =>
              options.UseNpgsql(webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection"))
            );

            #region Comment For Db Radis

            //webApplicationBuilder.Services.AddSingleton<IConnectionMultiplexer>((serveceProvider) => 
            //{
            //    var connection = webApplicationBuilder.Configuration.GetConnectionString("Redis");
            //    return ConnectionMultiplexer.Connect(connection);
            //}); 

            #endregion
           
            //ApplicationServicesExtensions.AddApplicationServices(webApplicationBuilder.Services);
            webApplicationBuilder.Services.AddApplicationServices();

            #endregion


            var app = webApplicationBuilder.Build();

            using var scope = app.Services.CreateScope();
            
                var services = scope.ServiceProvider;
                var _dbcontext = services.GetRequiredService<StoreContext>();
                var LoggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
               await _dbcontext.Database.MigrateAsync();
                await StoreContextSeed.SeedAsync(_dbcontext);

            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex);
                var logger = LoggerFactory.CreateLogger<Program>();
                logger.LogError(Ex, "error here");
            }

            #region Try Finally InBackground for Using Keyword

            ///try
            ///{
            ///    var services = scope.ServiceProvider;
            ///    var _dbcontext = services.GetRequiredService<StoreContext>();
            ///    _dbcontext.Database.MigrateAsync(); 
            ///}
            ///finally
            ///{
            ///    scope.Dispose();
            ///}

            #endregion


            #region Configure Kestreal Middklewares

            app.UseMiddleware<ExceptionMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSweggerAppMiddleWarw();
            }

            app.UseStatusCodePagesWithRedirects("/errors/{0}");
            app.UseHttpsRedirection();
            //app.UseStaticFiles();
            app.UseAuthorization();


            app.MapControllers();

            #endregion
          
            app.Run();
        }
    }
}
