
namespace Talabat.Api.Soluation2a.Errors
{
    public class ApiRespone
    {
        public int statusCode { get; set; }
        public string message { get; set; }

        public ApiRespone(int statusCodee , string? messagee = null)
        {
            statusCode = statusCodee;
            message = messagee ?? getdefaultmessageforStatusCode(statusCodee);
        }

        private string? getdefaultmessageforStatusCode(int statusCodee)
        {

            ///var messag = string.Empty;
            ///switch (statusCodee)
            ///{
            ///    case 400:
            ///        messag = "Bad Request";
            ///        break; 
            ///    case 401:
            ///        messag = "UnAuthorized";
            ///        break;   
            ///    default:
            ///        messag = null; 
            ///        break;
            ///}
            ///return messag;


            return statusCodee switch
            {
                400 => " A Bad Request You Have Made ",
                401 => "UnAuthorized , You Are Note ",
                404 => " Resourse Note Found ",
                500 => "  Dam , It Is About Data زفت  ",
                _   => null,
            };
        }
    }
}
