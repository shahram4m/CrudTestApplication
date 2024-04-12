using CrudTestApplication.Core.Entities;
using CrudTestApplication.Core.Repositories;
using CrudTestApplication.Core.Specifications;
using CrudTestApplication.Infrastructure.Data;
using CrudTestApplication.Infrastructure.Repository.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudTestApplication.Infrastructure.Repository
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(CrudTestApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Customer>> GetCustomerListAsync()
        {
            //var spec = new CustomerWithAccountNumerSpecification();
            //return await GetAsync(spec);

            // second way
            return await GetAllAsync();
        }

        public async Task<IEnumerable<Customer>> GetCustomerByNameAsync(string customerName)
        {
            var spec = new CustomerWithCategorySpecification(customerName);
            return await GetAsync(spec);

            // second way
            // return await GetAsync(x => x.CustomerName.ToLower().Contains(customerName.ToLower()));

            // third way
            //return await _dbContext.Customers
            //    .Where(x => x.CustomerName.Contains(customerName))
            //    .ToListAsync();
        }

        public async Task<IEnumerable<Customer>> GetCustomerByAccountNumberAsync(string accountNumber)
        {
            return await _dbContext.Customers
                .Where(x => x.BankAccountNumber == accountNumber)
                .ToListAsync();
        }

        public async Task<IEnumerable<Customer>> GetCustomerByCombineValueAsync(string firstName, string lastName, DateTime dateOfBirth)
        {
            return await _dbContext.Customers
                .Where(x => x.FirstName == firstName && x.LastName==lastName && x.DateOfBirth == dateOfBirth)
                .ToListAsync();
        }
    }
}
