namespace MiniCRM.Services.Data.Contracts
{
    using System.Threading.Tasks;

    public interface IAddressService
    {
        Task<int> CreateAsync(string city, string street, int? zipCode = null);
    }
}
