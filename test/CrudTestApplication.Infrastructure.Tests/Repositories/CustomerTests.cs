using CrudTestApplication.Infrastructure.Data;
using CrudTestApplication.Infrastructure.Repository;
using CrudTestApplication.Infrastructure.Tests.Builders;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace CrudTestApplication.Infrastructure.Tests.Repositories
{
    public class CustomerTests
    {
        private readonly CrudTestApplicationContext _aspnetRunContext;
        private readonly CustomerRepository _customerRepository;
        private readonly ITestOutputHelper _output;
        private CustomerBuilder CustomerBuilder { get; } = new CustomerBuilder();


        public CustomerTests(ITestOutputHelper output)
        {
            _output = output;
            var dbOptions = new DbContextOptionsBuilder<CrudTestApplicationContext>()
                .UseInMemoryDatabase(databaseName: "CrudTestApplication")
                .Options;
            _aspnetRunContext = new CrudTestApplicationContext(dbOptions);
            _customerRepository = new CustomerRepository(_aspnetRunContext);
        }

        [Fact]
        public async Task Get_Existing_Customer()
        {
            var existingCustomer = CustomerBuilder.WithDefaultValues();
            _aspnetRunContext.Customers.Add(existingCustomer);           
            _aspnetRunContext.SaveChanges();

            var customerId = existingCustomer.Id;
            _output.WriteLine($"CustomerId: {customerId}");

            var customerFromRepo = await _customerRepository.GetByIdAsync(customerId);
            Assert.Equal(CustomerBuilder.TestCustomerId, customerFromRepo.Id);
        }

        [Fact]
        public async Task Get_Customer_By_Name()
        {
            var existingCustomer = CustomerBuilder.WithDefaultValues();
            _aspnetRunContext.Customers.Add(existingCustomer);
            // GetCustomerByNameAsync spec required Category, because it is included Category entity so it should be exist


            _aspnetRunContext.SaveChanges();
            var customerName = existingCustomer.FirstName;
            _output.WriteLine($"CustomerName: {customerName}");
            
            var customerListFromRepo = await _customerRepository.GetCustomerByNameAsync(customerName);
            Assert.Equal(CustomerBuilder.TestFisrtName, customerListFromRepo.ToList().First().FirstName);
        }
    }
}
