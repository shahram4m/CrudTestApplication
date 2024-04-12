using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrudTestApplication.Core.Entities;
using CrudTestApplication.Core.Interfaces;
using CrudTestApplication.Core.Repositories;
using CrudTestApplication.Application.Models;
using CrudTestApplication.Application.Mapper;
using CrudTestApplication.Application.Interfaces;

namespace CrudTestApplication.Application.Services
{
    // TODO : add validation , authorization, logging, exception handling etc. -- cross cutting activities in here.
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IAppLogger<CustomerService> _logger;

        public CustomerService(ICustomerRepository customerRepository, IAppLogger<CustomerService> logger)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<CustomerModel>> GetCustomerList()
        {
            var customerList = await _customerRepository.GetCustomerListAsync();
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<CustomerModel>>(customerList);
            return mapped;
        }

        public async Task<CustomerModel> GetCustomerById(int customerId)
        {
            var customer = await _customerRepository.GetByIdAsync(customerId);
            var mapped = ObjectMapper.Mapper.Map<CustomerModel>(customer);
            return mapped;
        }

        public async Task<IEnumerable<CustomerModel>> GetCustomerByName(string customerName)
        {
            var customerList = await _customerRepository.GetCustomerByNameAsync(customerName);
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<CustomerModel>>(customerList);
            return mapped;
        }

        public async Task<IEnumerable<CustomerModel>> GetCustomerByAccountNumber(string accountNumber)
        {
            var customerList = await _customerRepository.GetCustomerByAccountNumberAsync(accountNumber);
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<CustomerModel>>(customerList);
            return mapped;
        }

        public async Task<IEnumerable<CustomerModel>> GetCustomerByCombineValue(string firstName, string lastName, DateTime dateOfBirth)
        {
            var customerList = await _customerRepository.GetCustomerByCombineValueAsync(firstName, lastName, dateOfBirth);
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<CustomerModel>>(customerList);
            return mapped;
        }


        public async Task<CustomerModel> Create(CustomerModel customerModel)
        {
            await ValidateCustomerIfExist(customerModel);

            var mappedEntity = ObjectMapper.Mapper.Map<Customer>(customerModel);
            if (mappedEntity == null)
                throw new ApplicationException($"Entity could not be mapped.");

            var newEntity = await _customerRepository.AddAsync(mappedEntity);
            _logger.LogInformation($"Entity successfully added - CrudTestApplicationAppService");

            var newMappedEntity = ObjectMapper.Mapper.Map<CustomerModel>(newEntity);
            return newMappedEntity;
        }

        public async Task Update(CustomerModel customerModel)
        {
            ValidateCustomerIfNotExist(customerModel);
            
            var editCustomer = await _customerRepository.GetByIdAsync(customerModel.Id);
            if (editCustomer == null)
                throw new ApplicationException($"Entity could not be loaded.");

            ObjectMapper.Mapper.Map<CustomerModel, Customer>(customerModel, editCustomer);

            await _customerRepository.UpdateAsync(editCustomer);
            _logger.LogInformation($"Entity successfully updated - CrudTestApplicationAppService");
        }

        public async Task Delete(CustomerModel customerModel)
        {
            ValidateCustomerIfNotExist(customerModel);
            var deletedCustomer = await _customerRepository.GetByIdAsync(customerModel.Id);
            if (deletedCustomer == null)
                throw new ApplicationException($"Entity could not be loaded.");

            await _customerRepository.DeleteAsync(deletedCustomer);
            _logger.LogInformation($"Entity successfully deleted - CrudTestApplicationAppService");
        }

        private async Task ValidateCustomerIfExist(CustomerModel customerModel)
        {
            var existingEntity = await _customerRepository.GetByIdAsync(customerModel.Id);
            if (existingEntity != null)
                throw new ApplicationException($"{customerModel.ToString()} with this id already exists");
        }

        private void ValidateCustomerIfNotExist(CustomerModel customerModel)
        {
            var existingEntity = _customerRepository.GetByIdAsync(customerModel.Id);
            if (existingEntity == null)
                throw new ApplicationException($"{customerModel.ToString()} with this id is not exists");
        }


    }
}
