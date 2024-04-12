using CrudTestApplication.Core.Entities;
using System;
using Xunit;

namespace CrudTestApplication.Core.Tests.Entities
{
    public class CustomerTests
    {
        private int _testCustomerId = 2;
        private string _testFistName = "alex";
        private string _testLastName = "Alex";
        private string _testPhoneNumber = "004451564564";
        private string _testEmail = "Alex@gmail.com";
        private string _testAccountNumber = "214214124";
        private DateTime _testDateOfBirth = DateTime.Now;

        [Fact]
        public void Create_Customer()
        {
            var customer = Customer.Create(_testCustomerId, _testFistName, _testLastName, _testDateOfBirth,  _testPhoneNumber, _testEmail, _testAccountNumber);

            Assert.Equal(_testCustomerId, customer.Id);
            Assert.Equal(_testFistName, customer.FirstName);
            Assert.Equal(_testLastName, customer.LastName);
            Assert.Equal(_testPhoneNumber, customer.PhoneNumber);
            Assert.Equal(_testEmail, customer.Email);
            Assert.Equal(_testAccountNumber, customer.BankAccountNumber);
            Assert.Equal(_testDateOfBirth, customer.DateOfBirth);
        }
    }
}
