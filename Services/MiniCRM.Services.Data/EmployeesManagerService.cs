using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace MiniCRM.Services.Data
{
    using System.Threading.Tasks;

    using MiniCRM.Data.Common.Repositories;
    using MiniCRM.Data.Models;
    using MiniCRM.Services.Data.Contracts;
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
            var address = await this.addressService.CreateAsync(input.Country, input.City, input.Street, input.ZipCode);
            var jobTitle = await this.jobTitlesService.CreateAsync(input.JobTitle);

            if (this.employersRepository.All().Select(x => x.PhoneNumber).Contains(input.Phone))
            {
                throw new Exception($"Phone {input.Phone} is already in use from another employer in your company.");
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
                PhoneNumber = input.Phone,
            };

            await this.employersRepository.AddAsync(employer);
            return await this.employersRepository.SaveChangesAsync();
        }
    }
}
