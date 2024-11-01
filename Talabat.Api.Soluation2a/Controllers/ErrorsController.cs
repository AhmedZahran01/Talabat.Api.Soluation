using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Api.Soluation2a.Errors;

namespace Talabat.Api.Soluation2a.Controllers
{
    [Route("errors/{Code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi =true)]
    public class ErrorsController : ControllerBase
    {
        public ActionResult error(int code) 
        { 
            return NotFound( new ApiRespone(code , " Not Found End Pount s" ) );
        }

    }
}
