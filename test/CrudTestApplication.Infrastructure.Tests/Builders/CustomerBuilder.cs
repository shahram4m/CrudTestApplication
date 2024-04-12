using CrudTestApplication.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrudTestApplication.Infrastructure.Tests.Builders
{
    public class CustomerBuilder
    {
        private Customer _customer;
        public int TestCustomerId => 5;
        public string TestFisrtName => "Test Customer Name";
        public string TestLastName => "Test Customer Last Name";
        public DateTime TestDateOfBirth => DateTime.Now;
        public string TestPhoneNumber => "54654654";
        public string TestEmail => "Alex@gmail.com";
        public string TestBankAccountNumber => "235235235";

        public CustomerBuilder()
        {
            _customer = WithDefaultValues();

        }

        public Customer Build()
        {
            return _customer;
        }

        public Customer WithDefaultValues()
        {
            return new Customer(); // Customer.Create(TestCustomerId, TestCategoryId, TestCustomerName);
        }

        public Customer WithAllValues()
        {
            return Customer.Create(TestCustomerId, TestFisrtName, TestLastName, TestDateOfBirth, TestPhoneNumber, TestEmail, TestBankAccountNumber);           
        }
    }
}
