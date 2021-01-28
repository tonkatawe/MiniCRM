namespace MiniCRM.Services.Data.Contracts
{
    using System.Threading.Tasks;

    public interface IJobTitlesService
    {
        Task<int> CreateAsync(string name);
    }
}
