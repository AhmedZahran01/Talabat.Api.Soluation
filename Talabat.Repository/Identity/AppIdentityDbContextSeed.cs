using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities.Identity;

namespace Talabat.Repository.Identity
{
    public static class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> _userManager)
        {
            if (_userManager.Users.Count() == 0)
            {
                var user = new AppUser()
                {
                    DisplayName = "Ahmed Zahran",
                    Email = "AhmedZahran@gmail.com",
                    UserName = "Ahmed.Zeno",
                    PhoneNumber = " 01122288282",
                     
                };
                await _userManager.CreateAsync(user ,"pass@10Asssss");
            }
        }
    }
}
