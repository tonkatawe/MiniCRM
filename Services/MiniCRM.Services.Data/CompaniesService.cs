using System.Linq;
using Microsoft.EntityFrameworkCore;
using MiniCRM.Services.Mapping;

namespace MiniCRM.Services.Data
{
    using System.Threading.Tasks;

    using MiniCRM.Data.Common.Repositories;
    using MiniCRM.Data.Models;
    using MiniCRM.Services.Data.Contracts;
    using MiniCRM.Web.ViewModels.Companies;

    public class CompaniesService : ICompaniesService
    {
        private readonly IDeletableEntityRepository<Company> companyRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IIndustriesService industriesService;
        private readonly IAddressService addressService;

        public CompaniesService(
            IDeletableEntityRepository<Company> companyRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository,
            IIndustriesService industriesService,
            IAddressService addressService)
        {
            this.companyRepository = companyRepository;
            this.userRepository = userRepository;
            this.industriesService = industriesService;
            this.addressService = addressService;
        }

        public async Task<int> CreateAsync(CompanyCreateModel input)
        {
            var industryId = await this.industriesService.CreateAsync(input.IndustryName);
            var addressId = await this.addressService.CreateAsync(input.Country, input.City, input.Street, input.ZipCode);

            var user = await this.userRepository
                .All()
                .Where(x => x.Id == input.UserId)
                .FirstOrDefaultAsync();

            var company = new Company
            {
                Name = input.Name,
                Description = input.Description,
                IsPublic = input.IsPublic,
                IndustryId = industryId,
                AddressId = addressId,
            };

            await this.companyRepository.AddAsync(company);
            await this.companyRepository.SaveChangesAsync();
            user.Company = company;

            this.userRepository.Update(user);

            return await this.userRepository.SaveChangesAsync();
        }

        public Task<T> GetByIdAsync<T>(string id)
        {
            var query = this.companyRepository
                .All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

            return query;
        }
    }
}
