using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Talabat.core.Entities.Identity;
using Talabat.core.Services.Contract;
using Talabat.Repository.Identity;
using Talabat.Services;

namespace Talabat.Api.Soluation2a.Extensions
{
    public static class IdentityServicesExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services,
                      IConfiguration configuration)
        {
            services.AddScoped(typeof(IAuthService), typeof(AuthService));

            #region AddIdentity Region

            //webApplicationBuilder.Services.AddIdentity<AppUser, IdentityRole>();

            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                ///options.Password.RequiredUniqueChars = 2;
                ///options.Password.RequireNonAlphanumeric = false;

            }).AddEntityFrameworkStores<AppIdentityDbContext>();

            #endregion

            #region Add Swagger Generation Region

            services.AddSwaggerGen(c =>
            { 
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version="v1" });

                // Add a security definition for Bearer token
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Please enter 'Bearer' [space] and then" +
                    " your token in the text input below.\n\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                // Add security requirement for the swagger documentation
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });


            }) .AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidAudience = configuration["JWT:ValidAudience"],
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JWT:ValidIssuer"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey
                                            (Encoding.UTF8.GetBytes(configuration["JWT:SecritKey"])),

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromDays(double.Parse(configuration["JWT:DurationInDays"])),
                };
            });



            #endregion


            #region  Comment Region

            #region Comment Add AuthenticationBearer Region
            #region MyRegion

            ///services.AddAuthentication("Bearer").AddJwtBearer(options => 
            ///{ 
            ///}) .AddJwtBearer("", f =>
            ///{ 
            ///}); 

            #endregion

            ////services.AddAuthentication("Bearer").AddJwtBearer(options =>
            ///services.AddAuthentication (JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
            ///    options => {
            ///    //options.ForwardSignIn = "";
            ///    options.TokenValidationParameters = new TokenValidationParameters()
            ///    {
            ///        ValidateAudience = true,
            ///        ValidAudience = configuration["JWT:ValidAudience"],
            ///        ValidateIssuer = true,
            ///        ValidIssuer = configuration["JWT:ValidIssuer"],
            ///        ValidateIssuerSigningKey = true,
            ///        IssuerSigningKey = new SymmetricSecurityKey
            ///                                (Encoding.UTF8.GetBytes(configuration["JWT:SecritKey"])),
            ///
            ///        ValidateLifetime = true,
            ///        ClockSkew = TimeSpan.FromDays(double.Parse( configuration["JWT:DurationInDays"])),
            ///    };
            ///});

            #endregion

            #region Add Authentication Region

            ///services.AddAuthentication(opt =>
            ///   {
            ///       opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            ///       opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; 
            ///   }).AddJwtBearer(options =>
            ///      {
            ///          options.TokenValidationParameters = new TokenValidationParameters()
            ///          {
            ///              ValidateAudience = true,
            ///              ValidAudience = configuration["JWT:ValidAudience"],
            ///              ValidateIssuer = true,
            ///              ValidIssuer = configuration["JWT:ValidIssuer"],
            ///              ValidateIssuerSigningKey = true,
            ///              IssuerSigningKey = new SymmetricSecurityKey
            ///                                      (Encoding.UTF8.GetBytes(configuration["JWT:SecritKey"])), 
            ///              ValidateLifetime = true,
            ///              ClockSkew = TimeSpan.FromDays(double.Parse(configuration["JWT:DurationInDays"])),
            ///          };
            ///   });

            #endregion

            #region Comment Momakan Region


            ///services.AddSwaggerGen(c =>
            /// { 
            ///     c.SwaggerDoc("v1" ,new OpenApiInfo { Title = "My API" }); 
            ///     // Add a security definition for Bearer token
            ///     c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            ///     {
            ///         In = ParameterLocation.Header,
            ///         Description = "Please enter 'Bearer' [space] and then your token in the text input below.\n\nExample: \"Bearer 12345abcdef\"",
            ///         Name = "Authorization",
            ///         Type = SecuritySchemeType.ApiKey
            ///     });    
            ///     // Add security requirement for the swagger documentation
            ///     c.AddSecurityRequirement(new OpenApiSecurityRequirement
            ///     {
            ///         { new OpenApiSecurityScheme
            ///             {
            ///                 Reference = new OpenApiReference
            ///                 {
            ///                     Type = ReferenceType.SecurityScheme,
            ///                     Id = "Bearer"
            ///                 }
            ///             },
            ///             new string[] { }
            ///         }
            ///     });
            /// });


            #endregion

            #endregion


            return services;
        }
    }
}
