using Microsoft.EntityFrameworkCore;

namespace MiniCRM.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using MiniCRM.Data.Common.Repositories;
    using MiniCRM.Data.Models;
    using MiniCRM.Services.Data.Contracts;
    using MiniCRM.Services.Mapping;
    using MiniCRM.Web.ViewModels.Employees;

    public class EmployeesManagerService : IEmployeesManagerService
    {
        private readonly IDeletableEntityRepository<Employer> employersRepository;
        private readonly IAddressService addressService;
        private readonly IJobTitlesService jobTitlesService;

        public EmployeesManagerService(
            IDeletableEntityRepository<Employer> employersRepository,
            IAddressService addressService,
            IJobTitlesService jobTitlesService)
        {
            this.employersRepository = employersRepository;
            this.addressService = addressService;
            this.jobTitlesService = jobTitlesService;
        }

        public async Task<int> CreateAsync(EmployerCreateModel input, string companyId)
        {
            var address = await this.addressService.CreateAsync(input.AddressCountry, input.AddressCity, input.AddressStreet, input.AddressZipCode);
            var jobTitle = await this.jobTitlesService.CreateAsync(input.JobTitleName);

            if (this.employersRepository.All().Select(x => x.PhoneNumber).Contains(input.PhoneNumber))
            {
                throw new Exception($"PhoneNumber {input.PhoneNumber} is already in use from another employer in your company.");
            }

            if (this.employersRepository.All().Select(x => x.Email).Contains(input.Email))
            {
                throw new Exception($"Email {input.Email} is already in use from another employer in your company.");
            }

            var employer = new Employer
            {
                FirstName = input.FirstName,
                MiddleName = input.MiddleName,
                LastName = input.LastName,
                CompanyId = companyId,
                AddressId = address,
                JobTitleId = jobTitle,
                Email = input.Email,
                PhoneNumber = input.PhoneNumber,
                OwnerId = input.OwnerId,
            };

            await this.employersRepository.AddAsync(employer);
            return await this.employersRepository.SaveChangesAsync();
        }

        public async Task<T> GetEmployerAsync<T>(int id)
        {
            var employer = await this.employersRepository
                .All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

            return employer;
        }

        public IQueryable<T> GetAll<T>(string companyId)
        {
            var query = this.employersRepository
                .All()
                .Where(x => x.CompanyId == companyId)
                .To<T>()
                .AsQueryable();

            return query;
        }

        public async Task ChangeAccountStatusAsync(int id, bool hasAccount, string accountId)
        {
            var employer = await this.employersRepository
                .All()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            employer.HasAccount = hasAccount;
            employer.AccountId = accountId;

            this.employersRepository.Update(employer);

            await this.employersRepository.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var employer = await this.employersRepository
                .All()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            employer.HasAccount = false;
            employer.AccountId = null;
            this.employersRepository.Delete(employer);

            return await this.employersRepository.SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync<T>(int employerId)
        {
            var employer = await this.employersRepository
                .All()
                .Where(x => x.Id == employerId)
                .To<T>()
                .FirstOrDefaultAsync();

            return employer;
        }

        public async Task<int> UpdateAsync(EmployerEditModel input)
        {
            var employer = await this.employersRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == input.Id);



            if (this.employersRepository.All().Select(x => x.PhoneNumber).Contains(input.PhoneNumber) && input.PhoneNumber != employer.PhoneNumber)
            {
                throw new Exception($"PhoneNumber {input.PhoneNumber} is already in use from another employer in your company.");
            }
            else
            {
                employer.PhoneNumber = input.PhoneNumber;
            }

            if (this.employersRepository.All().Select(x => x.Email).Contains(input.Email) && input.Email != employer.Email)
            {
                throw new Exception($"Email {input.Email} is already in use from another employer in your company.");
            }
            else
            {
                employer.Email = input.Email;
            }

            employer.FirstName = input.FirstName;
            employer.MiddleName = input.MiddleName;
            employer.LastName = input.LastName;


            await this.addressService.UpdateAsync(employer.AddressId, input.AddressCountry, input.AddressCity, input.AddressStreet,
                input.AddressZipCode);

            employer.JobTitleId = await this.jobTitlesService.CreateAsync(input.JobTitleName);

            this.employersRepository.Update(employer);

            return await this.employersRepository.SaveChangesAsync();
        }
    }
}
