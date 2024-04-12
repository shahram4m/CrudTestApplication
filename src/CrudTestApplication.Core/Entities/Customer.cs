using CrudTestApplication.Core.Entities.Base;
using Microsoft.VisualBasic;
using System;

namespace CrudTestApplication.Core.Entities
{
    public class Customer : Entity
    {
        public Customer()
        {
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }

        public static Customer Create(int customerId, string firstName, string lastName, DateTime dateOfBirth, string phoneNumber, string email, string BankAccountNumber)
        {
            var customer = new Customer
            {
                Id = customerId,
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth,
                PhoneNumber = phoneNumber,
                Email = email,
                BankAccountNumber = BankAccountNumber,
            };
            return customer;
        }
    }
}
