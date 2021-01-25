namespace MiniCRM.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using MiniCRM.Web.ViewModels.Companies;

    public interface ICompaniesService
    {
        Task<int> CreateAsync(CompanyCreateModel input);

        Task<T> GetByIdAsync<T>(string id);

    }
}
