using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;
using StackExchange.Redis;
using Talabat.Api.Soluation2a.Errors;
using Talabat.Api.Soluation2a.Extensions;
using Talabat.Api.Soluation2a.Helpers;
using Talabat.Api.Soluation2a.MiddleWares;
using Talabat.core.Entities;
using Talabat.core.Entities.Identity;
using Talabat.core.Repostories.Contract;
using Talabat.core.Services.Contract;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;
using Talabat.Services;

namespace Talabat.Api.Soluation2a
{
    public class Program
    {

        public static async Task  Main(string[] args)
        {
            #region Comment User test
                           //{
                           //                      "displayName": "T10",
                           //        "email": "use10@example.com",
                           //        "phoneNumber": "10101010101010101010101010101010",
                           //        "password": "Aa@21dsfdsfsd"
                           //}

            #endregion
           
            var webApplicationBuilder = WebApplication.CreateBuilder(args);
           
            #region Configure Services

            // Add services to the container.

            webApplicationBuilder.Services.AddControllers();

            webApplicationBuilder.Services.AddSweggerServices();

            webApplicationBuilder.Services.AddDbContext<StoreContext>(options =>
              options.UseNpgsql(webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection"))
            );

            webApplicationBuilder.Services.AddDbContext<AppIdentityDbContext>(optionsBuilder =>
            optionsBuilder.UseNpgsql(webApplicationBuilder.Configuration.GetConnectionString("IdentityConnection"))
            );

            #region Comment For Db Radis

            webApplicationBuilder.Services.AddSingleton<IConnectionMultiplexer>((serveceProvider) =>
            {
                var connection = webApplicationBuilder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connection);
            });

            #endregion

            //ApplicationServicesExtension s.AddApplicationServices(webApplicationBuilder.Services);
            webApplicationBuilder.Services.AddApplicationServices();
            webApplicationBuilder.Services.AddIdentityServices(webApplicationBuilder.Configuration);
            webApplicationBuilder.Services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy", optionss =>
                {
                    optionss.AllowAnyOrigin().AllowAnyMethod().AllowAnyOrigin();
                    //optionss.AllowAnyOrigin().WithMethods("GET", "POST").WithOrigins("https:dsa");
                });
            });

            #endregion


            var app = webApplicationBuilder.Build();

            #region Logger Factory Region

            using var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;
            var _dbcontext = services.GetRequiredService<StoreContext>();
            var _Identitydbcontext = services.GetRequiredService<AppIdentityDbContext>();
            var LoggerFactory = services.GetRequiredService<ILoggerFactory>();

            #endregion

            #region Migrate Region

            try
            {
                await _dbcontext.Database.MigrateAsync();
                await StoreContextSeed.SeedAsync(_dbcontext);

                await _Identitydbcontext.Database.MigrateAsync();
                var _Usermanager = services.GetRequiredService<UserManager<AppUser>>();
                await AppIdentityDbContextSeed.SeedUserAsync(_Usermanager);

            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex);
                var logger = LoggerFactory.CreateLogger<Program>();
                logger.LogError(Ex, "error here");
            }
             
            #endregion
        
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
            app.UseStaticFiles();
 
            app.UseCors("MyPolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            #endregion
          
            app.Run();
        }
    }
}
