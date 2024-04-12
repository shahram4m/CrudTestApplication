using CrudTestApplication.Core.Entities;
using CrudTestApplication.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrudTestApplication.Core.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<IEnumerable<Customer>> GetCustomerListAsync();
        Task<IEnumerable<Customer>> GetCustomerByNameAsync(string customerName);
        Task<IEnumerable<Customer>> GetCustomerByAccountNumberAsync(string accountNumber);

        Task<IEnumerable<Customer>> GetCustomerByCombineValueAsync(string firstName, string lastName, DateTime dateOfBirth);

    }
}
