using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace MiniCRM.Services.Data
{
    using System;

    using MiniCRM.Data.Common.Repositories;
    using MiniCRM.Data.Models;
    using MiniCRM.Services.Data.Contracts;

    public class ValidationsService : IValidationsService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public ValidationsService(IDeletableEntityRepository<ApplicationUser> usersRepository, UserManager<ApplicationUser> userManager)
        {
            this.usersRepository = usersRepository;
            this.userManager = userManager;
        }

        public bool IsExistUserEmail(string email) =>
            this.userManager.Users.Any(x => x.Email == email);

        public bool IsExistUserPhoneNumber(string phoneNumber) =>
            this.userManager.Users.Any(x => x.PhoneNumber == phoneNumber);
    }
}
