using MiniCRM.Data.Common.Repositories;
using MiniCRM.Data.Models;
using MiniCRM.Services.Data.Contracts;
using MiniCRM.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniCRM.Services.Data
{
    public class OrdersService : IOrdersService
    {
        private readonly IDeletableEntityRepository<Sale> salesRepository;

        public OrdersService(IDeletableEntityRepository<MiniCRM.Data.Models.Sale> salesRepository)
        {
            this.salesRepository = salesRepository;
        }
        public IQueryable<T> GetCustomerOrders<T>(int customerId)
        {
            var query = this.salesRepository.All()
            .Where(x => x.CustomerId == customerId)
            .To<T>()
            .AsQueryable();

            return query;
        }

        public IQueryable<T> GetEmployerOrders<T>(int emoployerId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetAllOrders<T>(string ownerId)
        {
            var query = this.salesRepository
              .All()
              .Where(x => x.Customer.OwnerId == ownerId)
              .To<T>()
              .AsQueryable();

            return query;
        }
    }
}
