using LearnScapeCore.BusinessModels.identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnScapeInfrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedAUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "God",
                    Email = "God@test.com",
                    UserName = "God",
                    Address = new Address
                    {
                        FirstName = "God",
                        LastName = "God",
                        Street = "here",
                        City = "there",
                        State = "florida",
                        ZipCode = "34243"
                    }
                };

                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}
