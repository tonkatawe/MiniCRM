namespace MiniCRM.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using MiniCRM.Common;
    using MiniCRM.Data.Models;

    public class UsersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            await SeedUserAsync(userManager, GlobalConstants.SystemEmail, GlobalConstants.AdministratorRoleName);
        }

        private static async Task SeedUserAsync(UserManager<ApplicationUser> userManager, string userEmail, string userRole)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(x => x.Email == userEmail);
            if (user == null)
            {
                var administrator = new ApplicationUser
                {
                    UserName = "Tonkata",
                    Email = GlobalConstants.SystemEmail,
                    FirstName = "Anton",
                    PasswordHash = "AQAAAAEAACcQAAAAEHaqN70NIqrsFd8S/h7WTfpSYchKSWzNovMoB5ImIExoS163O6wWX+1dFH7X5wa5uA==",
                    ProfilePictureUrl = "http://res.cloudinary.com/dx479nsjv/image/upload/v1611569326/MiniCRM/ProfilePictures/fjyerpauo4ovokskashb.png",
                    MiddleName = "Petrov",
                    LastName = "Vlahov",
                    EmailConfirmed = true,
                };

                await userManager.CreateAsync(administrator);
                await userManager.AddToRoleAsync(administrator, userRole);
            }
        }
    }
}
