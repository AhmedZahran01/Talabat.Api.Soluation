using System.Net;
using System.Text.Json;
using Talabat.Api.Soluation2a.Errors;

namespace Talabat.Api.Soluation2a.MiddleWares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IHostEnvironment env;

        public ExceptionMiddleware(RequestDelegate next , ILogger<ExceptionMiddleware> logger , IHostEnvironment env)
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }

        public async Task InvokeAsync(HttpContext httpcontext)
        {
            try
            {
                //await next.Invoke(httpcontext);
                await next(httpcontext);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);

                httpcontext.Response.ContentType = "application/json";
                var statuscodeOfexception = (int)HttpStatusCode.InternalServerError;
                httpcontext.Response.StatusCode = statuscodeOfexception;
                //httpcontext.Response.StatusCode = 500;
                var details = ex.StackTrace.ToString();
                var respone = env.IsDevelopment()?
                    new ApiExceptionRespone(statuscodeOfexception , ex.Message , details)
                     : new ApiExceptionRespone(statuscodeOfexception) ;

                var optionsCamalCase = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var JsonResponse = JsonSerializer.Serialize(respone , optionsCamalCase);

                await httpcontext.Response.WriteAsync(JsonResponse);
               
            }
        }


    }
}
