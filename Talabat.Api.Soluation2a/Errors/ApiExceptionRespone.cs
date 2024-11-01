namespace Talabat.Api.Soluation2a.Errors
{
    public class ApiExceptionRespone : ApiRespone
    {
        public string? Details { get; set; }
        public ApiExceptionRespone(int statusCode , string? Message=null , string? details = null):base(statusCode, Message)
        {
            Details = details;
        }
    }
}
