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
        private readonly IIndustriesService industriesService;
        private readonly IAddressService addressService;

        public CompaniesService(
            IDeletableEntityRepository<Company> companyRepository,
            IIndustriesService industriesService,
            IAddressService addressService)
        {
            this.companyRepository = companyRepository;
            this.industriesService = industriesService;
            this.addressService = addressService;
        }

        public async Task<int> CreateAsync(CompanyCreateModel input)
        {
            var industryId = await this.industriesService.CreateAsync(input.IndustryName);
            var addressId = await this.addressService.CreateAsync(input.Country, input.City, input.Street, input.ZipCode);

            var company = new Company
            {
                UserId = input.UserId,
                Name = input.Name,
                Description = input.Description,
                IsPublic = input.IsPublic,
                IndustryId = industryId,
                AddressId = addressId,
            };

            await this.companyRepository.AddAsync(company);
            return await this.companyRepository.SaveChangesAsync();
        }
    }
}
