using CrudTestApplication.Application.Interfaces;
using CrudTestApplication.Application.Models;
using CrudTestApplication.Web.Interfaces;
using CrudTestApplication.Web.ViewModels;
using AutoMapper;
using Microsoft.Extensions.Logging;
using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PhoneNumbers;

namespace CrudTestApplication.Web.Services
{
    public class CustomerPageService : ICustomerPageService
    {
        private readonly ICustomerService _customerAppService;

        private readonly IMapper _mapper;
        private readonly ILogger<CustomerPageService> _logger;

        public CustomerPageService(ICustomerService customerAppService, IMapper mapper, ILogger<CustomerPageService> logger)
        {
            _customerAppService = customerAppService ?? throw new ArgumentNullException(nameof(customerAppService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<CustomerViewModel>> GetCustomers(string customerName)
        {
            if (string.IsNullOrWhiteSpace(customerName))
            {
                var list = await _customerAppService.GetCustomerList();
                var mapped = _mapper.Map<IEnumerable<CustomerViewModel>>(list);
                return mapped;
            }

            var listByName = await _customerAppService.GetCustomerByName(customerName);
            var mappedByName = _mapper.Map<IEnumerable<CustomerViewModel>>(listByName);
            return mappedByName;
        }

        public async Task<CustomerViewModel> GetCustomerById(int customerId)
        {
            var customer = await _customerAppService.GetCustomerById(customerId);
            var mapped = _mapper.Map<CustomerViewModel>(customer);
            return mapped;
        }

        public async Task<IEnumerable<CustomerViewModel>> GetCustomerByAccountNumber(string accountNumber)
        {
            var list = await _customerAppService.GetCustomerByAccountNumber(accountNumber);
            var mapped = _mapper.Map<IEnumerable<CustomerViewModel>>(list);
            return mapped;
        }

        public async Task<IEnumerable<CustomerViewModel>> GetCustomerByCombineValue(string firstName, string lastName, DateTime dateOfBirth)
        {
            var list = await _customerAppService.GetCustomerByCombineValue(firstName,  lastName, dateOfBirth);
            var mapped = _mapper.Map<IEnumerable<CustomerViewModel>>(list);
            return mapped;
        }
        
        public bool CheckCorrectBankAccountNumber(string phoneNumber)
        {
            var phoneNumberUtil = PhoneNumberUtil.GetInstance();
            var phoneNumberCheck = phoneNumberUtil.Parse(phoneNumber, "US");
            var isValid = phoneNumberUtil.IsValidNumber(phoneNumberCheck);
            if (!isValid)
                return false;

             return true;
        }

        public async Task<CustomerViewModel> CreateCustomer(CustomerViewModel customerViewModel)
        {
            var mapped = _mapper.Map<CustomerModel>(customerViewModel);
            if (mapped == null)
                throw new Exception($"Entity could not be mapped.");

            var phoneNumberUtil = PhoneNumberUtil.GetInstance();
            var phoneNumber = phoneNumberUtil.Parse(customerViewModel.PhoneNumber, "US");
            var isValid = phoneNumberUtil.IsValidNumber(phoneNumber);
            if (!isValid)
                throw new Exception($"Phone number not valid.");

            var existCustomerByAccountNumber = this.GetCustomerByAccountNumber(customerViewModel.BankAccountNumber);
            if (existCustomerByAccountNumber!= null)
                throw new Exception($"Accountnumber already exist.");


            //check customer for Firstname, Lastname, and DateOfBirth
            var existCustomerByCombineValue = this.GetCustomerByCombineValue(customerViewModel.FirstName, customerViewModel.LastName, customerViewModel.DateOfBirth);
            if (existCustomerByCombineValue != null)
                throw new Exception($"Customer already exist.");
            
            var entityDto = await _customerAppService.Create(mapped);
            _logger.LogInformation($"Entity successfully added - IndexPageService");

            var mappedViewModel = _mapper.Map<CustomerViewModel>(entityDto);
            return mappedViewModel;
        }

        public async Task UpdateCustomer(CustomerViewModel customerViewModel)
        {
            var mapped = _mapper.Map<CustomerModel>(customerViewModel);
            if (mapped == null)
                throw new Exception($"Entity could not be mapped.");

            var phoneNumberUtil = PhoneNumberUtil.GetInstance();
            var phoneNumber = phoneNumberUtil.Parse(customerViewModel.PhoneNumber, "US");
            var isValid = phoneNumberUtil.IsValidNumber(phoneNumber);
            if (!isValid)
                throw new Exception($"Phone number not valid.");

            await _customerAppService.Update(mapped);
            _logger.LogInformation($"Entity successfully added - IndexPageService");
        }

        public async Task DeleteCustomer(CustomerViewModel customerViewModel)
        {
            var mapped = _mapper.Map<CustomerModel>(customerViewModel);
            if (mapped == null)
                throw new Exception($"Entity could not be mapped.");

            await _customerAppService.Delete(mapped);
            _logger.LogInformation($"Entity successfully added - IndexPageService");
        }
    }
}
