using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Api.Soluation2a.DTOs;
using Talabat.Api.Soluation2a.Errors;
using Talabat.core.Entities;
using Talabat.core.Repostories.Contract;
using Talabat.Repository;

namespace Talabat.Api.Soluation2a.Controllers
{ 
    public class BasketController : BaseApiController
    {
        #region Comment For Db Radis


        #region Contructor Region 

        private readonly IBasketRepositoty _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepositoty basketRepository , IMapper mapper)
        {
            this._basketRepository = basketRepository;
            this._mapper = mapper;
        }

        #endregion

        #region Get Customer Basket Region

        //[HttpGet("id")]
        [HttpGet("GetById")]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);
            return Ok(basket ?? new CustomerBasket(id));

        }
        #endregion


        #region Update Customer Basket  Region

        [HttpPost("create or update")]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
        {
            var mappedBasket = _mapper.Map<CustomerBasketDto,CustomerBasket>(basket);

            var CreatedOrUpdatedbasket = await _basketRepository.UpdateBasketAsync(mappedBasket);
            if (CreatedOrUpdatedbasket == null)
            {
                return BadRequest(new ApiRespone(400));
            }
            return Ok(CreatedOrUpdatedbasket);

        }
        #endregion


        #region Delete Customer Basket Region

        [HttpDelete]
        public async Task DeleteBasketAsync(string id)
        {
            await _basketRepository.DeleteBasketAsync(id);
        }

        #endregion


        #endregion

    }
}
