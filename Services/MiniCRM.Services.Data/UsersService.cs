﻿namespace MiniCRM.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using MiniCRM.Common;
    using MiniCRM.Data.Common.Repositories;
    using MiniCRM.Data.Models;
    using MiniCRM.Services.Data.Contracts;
    using MiniCRM.Services.Mapping;
    using MiniCRM.Web.ViewModels;
    using MiniCRM.Web.ViewModels.Employees;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IJobTitlesService jobTitlesService;

        public UsersService(
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            UserManager<ApplicationUser> userManager,
            IJobTitlesService jobTitlesService)
        {
            this.usersRepository = usersRepository;
            this.userManager = userManager;
            this.jobTitlesService = jobTitlesService;
        }

        public async Task<T> GetUserAsync<T>(string userId)
        {
            var query = await this.usersRepository
                .All()
                .Where(x => x.Id == userId)
                .To<T>()
                .FirstOrDefaultAsync();

            return query;
        }

        public IQueryable<T> GetAllUser<T>(string userId)
        {
            var query = this.usersRepository
                .All()
                .Where(x => x.ParentId == userId)
                .To<T>()
                .AsQueryable();

            return query;
        }

        public async Task<int> DeleteAsync(string userId)
        {
            var user = await this.usersRepository.All().FirstOrDefaultAsync(x => x.Id == userId);

            var roles = await this.userManager.GetRolesAsync(user);

            await this.userManager.RemoveFromRolesAsync(user, roles);

            this.usersRepository.Delete(user);

            return await this.usersRepository.SaveChangesAsync();
        }

        public async Task<(string, string, string)> CreateAsync(UserCreateModel input, UserViewModel parent)
        {
            var jobTitle = await this.jobTitlesService.CreateAsync(input.JobTitle);

            var username = $"{input.FirstName.Substring(0, 1)}.{input.LastName}";
            var possibleUsername = await this.GenerateAvailableUsername(username);

            var employer = new ApplicationUser
            {
                UserName = possibleUsername,
                FirstName = input.FirstName,
                MiddleName = input.MiddleName,
                LastName = input.LastName,
                PhoneNumber = input.Phone,
                ParentId = parent.Id,
                Email = input.Email,
                CompanyId = parent.CompanyId,
                ProfilePictureUrl = @"https://res.cloudinary.com/dx479nsjv/image/upload/v1611663587/MiniCRM/ProfilePictures/default-profile-picture_cwgvhg.png",
                JobTitleId = jobTitle,
            };

            var employerPassword = Guid.NewGuid().ToString().Substring(0, 8);

            var result = await this.userManager.CreateAsync(employer, employerPassword);

            if (result.Succeeded)
            {
                await this.userManager.AddToRoleAsync(employer, GlobalConstants.EmployerUserRoleName);

                var token = await this.userManager.GenerateEmailConfirmationTokenAsync(employer);

                return (token, employerPassword, possibleUsername);
            }
            else
            {
                throw new Exception(result.ToString());
            }
        }

        private async Task<string> GenerateAvailableUsername(string possibleUsername)
        {
            var existingUsers = await this.usersRepository
                .All()
                .Where(u => u.UserName.StartsWith(possibleUsername))
                .ToListAsync();

            if (existingUsers.Count == 0)
            {
                return possibleUsername;
            }
            else
            {
                for (int i = 0; i < existingUsers.Count; i++)
                {
                    if (existingUsers.FirstOrDefault(u => u.UserName == $"{possibleUsername}{i + 1}") == null)
                    {
                        possibleUsername = $"{possibleUsername}{i + 1}";
                        break;
                    }
                }

                return possibleUsername;
            }
        }
    }
}
