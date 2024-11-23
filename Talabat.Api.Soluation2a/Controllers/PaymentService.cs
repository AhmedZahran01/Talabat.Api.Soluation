using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Api.Soluation2a.DTOs;
using Talabat.Api.Soluation2a.Errors;
using Talabat.core.Entities;
using Talabat.core.Services.Contract;

namespace Talabat.Api.Soluation2a.Controllers
{ 
    public class PaymentsController : BaseApiController
    {
        #region Constractor Region

        private readonly IPaymentService _paymentService;
        public PaymentsController(IPaymentService paymentService)
        {
            this._paymentService = paymentService;
        }

        #endregion

        #region CreateOrUpdatePaymentIntend Region

        [ProducesResponseType(typeof(CustomerBasket), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiRespone), StatusCodes.Status400BadRequest)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("basketId")]
        //[HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntend(string basketId)
        {
            var basket = await _paymentService.CreateOrUpdatePaymentIntend(basketId);
            if (basket == null) return BadRequest(new ApiRespone(400, "ther is an error in your basket"));
            return Ok(basket);
        }

        #endregion

    }
}
