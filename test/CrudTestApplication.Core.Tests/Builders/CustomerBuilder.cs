using CrudTestApplication.Core.Entities;
using System;
using System.Collections.Generic;

namespace CrudTestApplication.Core.Tests.Builders
{
    public class CustomerBuilder
    {
        public int CustomerId1 => 123;
        public int CustomerId2 => 124;
        public int CustomerId3 => 125;
        public string CustomerName1 => "CustomerX";
        public string CustomerName2 => "CustomerY";
        public string CustomerName3 => "CustomerZ";

        public string CustomerFistName = "alex";
        public string CustomerLastName = "Alex";
        public string CustomerPhoneNumber = "004451564564";
        public string CustomerEmail = "Alex@gmail.com";
        public string CustomerAccountNumber = "214214124";
        public DateTime CustomerDateOfBirth = DateTime.Now;


        public List<Customer> GetCustomerCollection()
        {
            return new List<Customer>()
            {
                Customer.Create(CustomerId1, CustomerFistName, CustomerLastName, CustomerDateOfBirth,  CustomerPhoneNumber, CustomerEmail, CustomerAccountNumber)
            };
        }
    }
}
