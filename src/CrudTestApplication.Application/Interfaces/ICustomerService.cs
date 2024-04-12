using CrudTestApplication.Application.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrudTestApplication.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerModel>> GetCustomerList();
        Task<CustomerModel> GetCustomerById(int customerId);
        Task<IEnumerable<CustomerModel>> GetCustomerByName(string customerName);
        Task<IEnumerable<CustomerModel>> GetCustomerByAccountNumber(string accountNumber);
        Task<IEnumerable<CustomerModel>> GetCustomerByCombineValue(string firstName, string lastName, DateTime dateOfBirth);
        Task<CustomerModel> Create(CustomerModel customerModel);
        Task Update(CustomerModel customerModel);
        Task Delete(CustomerModel customerModel);
    }
}
