using System.Threading.Tasks;
using MiniCRM.Web.ViewModels.Companies;

namespace MiniCRM.Services.Data.Contracts
{
    public interface ICompaniesService
    {
        Task<int> CreateAsync(CompanyCreateModel input);

    }
}
