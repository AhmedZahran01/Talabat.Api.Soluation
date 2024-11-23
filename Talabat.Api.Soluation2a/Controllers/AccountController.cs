using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.Api.Soluation2a.DTOs;
using Talabat.Api.Soluation2a.Errors;
using Talabat.Api.Soluation2a.Extensions;
using Talabat.core.Entities.Identity;
using Talabat.core.Services.Contract;

namespace Talabat.Api.Soluation2a.Controllers
{
    public class AccountController : BaseApiController
    {

        #region Constractor Region

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager
                                   , IAuthService authService, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this._authService = authService;
            this._mapper = mapper;
        }

        #endregion

        #region Login Region

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> login(LoginDto login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user == null)
            { return Unauthorized(new ApiRespone(401)); }

            var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);
            if (result.Succeeded is false)
            { return Unauthorized(new ApiRespone(401)); }

            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _authService.CreateTokenAsync(user, _userManager)
            });
        }

        #endregion

        #region Register Region

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            if (CheckEmailExists(model.Email).Result.Value)
                return BadRequest(new ApiValidationErrorRespone()
                { Errors = new string[] { "this email is already user !!" } });

            var user = new AppUser()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName = model.Email.Split("@")[0],
                PhoneNumber = model.PhoneNumber,
            };
            List<string> ckode = new List<string>();
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded is false)
            {
                foreach (var item in result.Errors)
                {
                    ckode.Add(item.Description);
                }
                //return BadRequest(new ApiRespone(400));
                return BadRequest(new ApiValidationErrorRespone()
                { Errors = ckode });

            }

            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _authService.CreateTokenAsync(user, _userManager)
            });
        }

        #endregion

        #region Get Current User Region

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);
            return Ok(new UserDto()
            {
                DisplayName = user.UserName,
                Email = user.Email,
                Token = await _authService.CreateTokenAsync(user, _userManager)
            });
        }

        #endregion

        #region Get User Address Region

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("Address")]
        public async Task<ActionResult<OrderAddressDto>> GetUserAddress()
        {
            var user = await _userManager.FindUserWithAddressAsync(User);
            var address = _mapper.Map<OrderAddressDto>(user.Address);
            return Ok(address);
        }

        #endregion

        #region Update User Address Region

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("Address")]
        public async Task<ActionResult<OrderAddressDto>> UpdateUserAddress(OrderAddressDto updateAddress)
        {
            var address = _mapper.Map<OrderAddressDto, Address>(updateAddress);
            var user = await _userManager.FindUserWithAddressAsync(User);
            address.Id = user.Address.Id;
            user.Address = address;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) return BadRequest(new ApiRespone(400));
            return Ok(address);

        }

        #endregion


        #region Check Email Exists  Region

        [HttpGet("EmailExists")]
        public async Task<ActionResult<bool>> CheckEmailExists(string email)
        {
            bool x = await _userManager.FindByEmailAsync(email) is not null;
            return x;
        }


        #endregion


    }
}
