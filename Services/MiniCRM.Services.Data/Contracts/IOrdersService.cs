namespace MiniCRM.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IOrdersService
    {
        IQueryable<T> GetCustomerOrders<T>(int customerId);

        IQueryable<T> GetEmployerOrders<T>(int emoployerId);

        IQueryable<T> GetAllOrders<T>(string ownerId);
    }
}
