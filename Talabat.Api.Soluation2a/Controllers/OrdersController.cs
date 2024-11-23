using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Security.Claims;
using Talabat.Api.Soluation2a.DTOs;
using Talabat.Api.Soluation2a.Errors;
using Talabat.core.Entities.OrderAggregates;
using Talabat.core.Services.Contract;
using Order = Talabat.core.Entities.OrderAggregates.Order;

namespace Talabat.Api.Soluation2a.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrdersController : BaseApiController
    { 
        #region Constractor Region
       
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService service , IMapper mapper)
        {
            _orderService = service;
            this._mapper = mapper;
        }

        #endregion
         
        #region Create Order Region
     
        [ProducesResponseType(typeof(OrderToReturnDto) ,StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiRespone) ,StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var address = _mapper.Map<OrderAddressDto, OrderAddress>(orderDto.ShippingAddress);
            var order = await _orderService.CreateOrderAsync( buyerEmail, orderDto.BasketId
                                                 , orderDto.DeliveryMethodId, address);
            if (order == null) return BadRequest(new ApiRespone(400));
            return Ok(_mapper.Map<Order , OrderToReturnDto>(order));

        }

        #endregion
         
        #region Get Orders For User Region

        [HttpGet("GetOrdersForUser")]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUser()
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var orders = await _orderService.GetOrdersforUserAsync(buyerEmail);
            return Ok(_mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(orders));

        }

        #endregion
         
        #region Get Order By User Region

        [ProducesResponseType(typeof(OrderToReturnDto), StatusCodes.Status200OK)] // StatusCodes class inside him Constant
        [ProducesResponseType(typeof(ApiRespone), StatusCodes.Status404NotFound)] // StatusCodes class inside him Constant
        [HttpGet("id")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderByUser(int id )
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);

            var order = await _orderService.GetOrderByIdforUserAsync(id, buyerEmail);
            if(order == null) return NotFound(new ApiRespone(404));

            return Ok(_mapper.Map<Order, OrderToReturnDto>(order));
        }

        #endregion
         
        #region Get All Delivery Methods Region
        
        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetdeliveryMethods()
        {
            var deliveryMethods =  await _orderService.GetDeliveryMethodsAsync();
             
            return  Ok(deliveryMethods);
        }

        #endregion
         
    }
}
