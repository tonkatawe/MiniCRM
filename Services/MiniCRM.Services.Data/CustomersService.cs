using System.Linq;
using Microsoft.EntityFrameworkCore;
using MiniCRM.Services.Mapping;

namespace MiniCRM.Services.Data
{
    using System.Threading.Tasks;

    using MiniCRM.Data.Common.Repositories;
    using MiniCRM.Data.Models;
    using MiniCRM.Services.Data.Contracts;
    using MiniCRM.Web.ViewModels.Customer;

    public class CustomersService : ICustomersService
    {
        private readonly IDeletableEntityRepository<Customer> customersRepository;
        private readonly IAddressService addressService;
        private readonly IJobTitlesService jobTitlesService;

        public CustomersService(
            IDeletableEntityRepository<Customer> customersRepository,
            IAddressService addressService,
            IJobTitlesService jobTitlesService)
        {
            this.customersRepository = customersRepository;
            this.addressService = addressService;
            this.jobTitlesService = jobTitlesService;
        }

        public async Task<int> CreateAsync(CustomerCreateModel input, string ownerId)
        {
            var address = await this.addressService.CreateAsync(input.AddressCountry, input.AddressCity, input.AddressStreet, input.AddressZipCode);
            var jobTitle = await this.jobTitlesService.CreateAsync(input.JobTitleName);
            var customer = new Customer
            {
                FirstName = input.FirstName,
                MiddleName = input.MiddleName,
                LastName = input.LastName,
                JobTitleId = jobTitle,
                AddressId = address,
                EmployerId = input.EmployerId,
                OwnerId = ownerId,
                Phone = input.PhoneNumber,
                Email = input.Email,
                AdditionalInfo = input.AdditionalInfo,
            };

            await this.customersRepository.AddAsync(customer);
            return await this.customersRepository.SaveChangesAsync();
        }

        public IQueryable<T> GetAll<T>(string ownerId)
        {
            var query = this.customersRepository
                .All()
                .Where(x => x.OwnerId == ownerId)
                .To<T>()
                .AsQueryable();

            return query;
        }

        public IQueryable<T> GetEmployerCustomers<T>(string ownerId, int? employerId)
        {
            var query = this.customersRepository
                .All()
                .Where(x => x.OwnerId == ownerId && x.EmployerId == employerId)
                .To<T>()
                .AsQueryable();

            return query;
        }

        public async Task<T> GetByIdAsync<T>(int id)
        {
            var customer = await this.customersRepository
                .All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

            return customer;
        }

        public async Task DeleteAsync(int id)
        {
            var customer = await this.customersRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == id);

            this.customersRepository.Delete(customer);

            await this.customersRepository.SaveChangesAsync();
        }
    }
}
