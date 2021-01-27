using System;
using System.Collections.Generic;
using MiniCRM.Common;
using MiniCRM.Data.Common.Repositories;

namespace MiniCRM.Services.Data
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using MiniCRM.Data.Models;
    using MiniCRM.Services.Data.Contracts;
    using MiniCRM.Web.ViewModels.Employees;

    public class EmployeesManagerService : IEmployeesManagerService
    {
        private readonly UserManager<ApplicationUser> userManager;

        public EmployeesManagerService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<KeyValuePair<string, string>> CreateAsync(EmployeeCreateModel input, string ownerId)
        {
            var owner = await this.userManager.FindByIdAsync(ownerId);
            //todo make job title service

            var employer = new ApplicationUser
            {
                UserName = input.UserName,
                FirstName = input.FirstName,
                MiddleName = input.MiddleName,
                LastName = input.LastName,
                PhoneNumber = input.Phone,
                Parent = owner,
                Email = input.Email,
                CompanyId = owner.CompanyId,
                ProfilePictureUrl = @"https://res.cloudinary.com/dx479nsjv/image/upload/v1611663587/MiniCRM/ProfilePictures/default-profile-picture_cwgvhg.png",
                JobTitle = new JobTitle { Name = input.JobTitle },
            };

            var employerPassword = Guid.NewGuid().ToString().Substring(0, 8);

            var result = await this.userManager.CreateAsync(employer, employerPassword);

            if (result.Succeeded)
            {
                await this.userManager.AddToRoleAsync(employer, GlobalConstants.EmployerUserRoleName);

                var token = await this.userManager.GenerateEmailConfirmationTokenAsync(employer);

                return new KeyValuePair<string, string>(token, employerPassword);
            }
            else
            {
                throw new Exception(result.ToString());
            }

        }
    }
}
