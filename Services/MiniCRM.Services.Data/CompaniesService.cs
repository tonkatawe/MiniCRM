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

        public CompaniesService(IDeletableEntityRepository<Company> companyRepository, IIndustriesService industriesService)
        {
            this.companyRepository = companyRepository;
            this.industriesService = industriesService;
        }

        public Task<int> CreateAsync(CompanyCreateModel input)
        {
            var industryId = this.industriesService.CreateAsync(input.IndustryName);
            var addressId = this.
            var company = new Company
            {
                UserId = input.UserId,
                Name = input.Name,
                IsPublic = input.IsPublic,
                IndustryId = industryId,
                AddressId = 
            };
        }
    }
}
