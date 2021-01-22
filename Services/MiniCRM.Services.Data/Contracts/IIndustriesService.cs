using System.Threading.Tasks;

namespace MiniCRM.Services.Data.Contracts
{
    public interface IIndustriesService
    {
        Task<int> CreateAsync(string name);
    }
}
