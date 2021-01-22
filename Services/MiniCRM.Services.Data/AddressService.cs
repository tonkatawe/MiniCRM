using MiniCRM.Data.Common.Repositories;
using MiniCRM.Data.Models;

namespace MiniCRM.Services.Data
{
    using System.Threading.Tasks;

    using MiniCRM.Services.Data.Contracts;

    public class AddressService : IAddressService
    {
        private readonly IRepository<Address> addressRepository;

        public AddressService(IRepository<Address> addressRepository)
        {
            this.addressRepository = addressRepository;
        }

        public async Task<int> CreateAsync(string country, string city, string street, int? zipCode = null)
        {
            var address = new Address
            {
                Country = country,
                City = city,
                Street = street,
                ZipCode = zipCode,
            };

            await this.addressRepository.AddAsync(address);

            return await this.addressRepository.SaveChangesAsync();
        }
    }
}
