
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Talabat.core.Entities.Identity;
using Talabat.core.Services.Contract;

namespace Talabat.Services
{
    public class AuthService : IAuthService
    {
        #region Constractor Region

        private readonly IConfiguration _configuration;
        public AuthService(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        #endregion
       
        #region Create Token Async Region

        public async Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> userManager)
        {
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.GivenName , user.UserName),
                //new Claim("UserName" , user.UserName) key is just string
                new Claim(ClaimTypes.Email , user.Email),
                //new Claim(ClaimTypes.NameIdentifier , user.Id),
                //new Claim(ClaimTypes.Role , user.Id),
            };

            var userRole = await userManager.GetRolesAsync(user);
            foreach (var role in userRole)
                authClaims.Add(new Claim(ClaimTypes.Role, role));

            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecritKey"]));
            var token = new JwtSecurityToken(

                audience: _configuration["JWT:ValidAudience"],
                issuer: _configuration["JWT:ValidIssuer"],
                expires: DateTime.UtcNow.AddDays(double.Parse(_configuration["JWT:DurationInDays"])),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256Signature)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        #endregion
  
    
    }
}
