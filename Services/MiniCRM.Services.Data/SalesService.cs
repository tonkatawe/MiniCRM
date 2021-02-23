using MiniCRM.Data.Common.Repositories;
using MiniCRM.Data.Models;

namespace MiniCRM.Services.Data
{
    using MiniCRM.Services.Data.Contracts;

    public class SalesService : ISalesService
    {
        private readonly IDeletableEntityRepository<Sale> salesRepository;

        public SalesService(IDeletableEntityRepository<Sale> salesRepository)
        {
            this.salesRepository = salesRepository;
        }
    }
}
