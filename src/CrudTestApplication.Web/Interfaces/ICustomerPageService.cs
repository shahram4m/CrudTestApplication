using CrudTestApplication.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrudTestApplication.Web.Interfaces
{
    public interface ICustomerPageService
    {
        Task<IEnumerable<CustomerViewModel>> GetCustomers(string customerName);
        Task<CustomerViewModel> GetCustomerById(int customerId);
        Task<IEnumerable<CustomerViewModel>> GetCustomerByAccountNumber(string accountNumber);
        Task<IEnumerable<CustomerViewModel>> GetCustomerByCombineValue(string firstName, string lastName, DateTime dateOfBirth);
        Task<CustomerViewModel> CreateCustomer(CustomerViewModel customerViewModel);
        Task UpdateCustomer(CustomerViewModel customerViewModel);
        Task DeleteCustomer(CustomerViewModel customerViewModel);

        bool CheckCorrectBankAccountNumber(string PhoneNumber);
    }
}
