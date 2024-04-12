using CrudTestApplication.Application.Exceptions;
using CrudTestApplication.Application.Services;
using CrudTestApplication.Core.Entities;
using CrudTestApplication.Core.Interfaces;
using CrudTestApplication.Core.Repositories;
using CrudTestApplication.Core.Repositories.Base;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace CrudTestApplication.Application.Tests.Services
{
    public class CustomerTests
    {
        // NOTE : This layer we are not loaded database objects, test functionality of application layer

        private Mock<ICustomerRepository> _mockCustomerRepository;
        private Mock<IAppLogger<CustomerService>> _mockAppLogger;

        public CustomerTests()
        {
            _mockCustomerRepository = new Mock<ICustomerRepository>();
            _mockAppLogger = new Mock<IAppLogger<CustomerService>>();
        }      

        [Fact]
        public async Task Get_Customer_List()
        {
            var customer1 = new Customer(); //Customer.Create(It.IsAny<int>(), 0, It.IsAny<string>());
            var customer2 = new Customer();  //Customer.Create(It.IsAny<int>(), 0, It.IsAny<string>());

            //category.AddCustomer(customer1.Id, It.IsAny<string>());
            //category.AddCustomer(customer2.Id, It.IsAny<string>());

            _mockCustomerRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(customer1);
            _mockCustomerRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(customer2);

            var customerService = new CustomerService(_mockCustomerRepository.Object, _mockAppLogger.Object);
            var customerList = await customerService.GetCustomerList();

            _mockCustomerRepository.Verify(x => x.GetCustomerListAsync(), Times.Once);
        }

        [Fact]
        public async Task Create_New_Customer()
        {
            var customer = new Customer();  //Customer.Create(It.IsAny<int>(), It.IsAny<string>());
            Customer nullCustomer = null; // we gave null customer in order to create new one, if you give real customer it returns already existing error

            _mockCustomerRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(nullCustomer);
            _mockCustomerRepository.Setup(x => x.AddAsync(customer)).ReturnsAsync(customer);            

            var customerService = new CustomerService(_mockCustomerRepository.Object, _mockAppLogger.Object);
            var createdCustomerDto = await customerService.Create(new Models.CustomerModel { Id = customer.Id, FirstName = customer.FirstName});

            _mockCustomerRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
            _mockCustomerRepository.Verify(x => x.AddAsync(customer), Times.Once);
        }

        [Fact]
        public async Task Create_New_Customer_Validate_If_Exist()
        {

            var customer = new Customer(); //Customer.Create(It.IsAny<int>(), 0, It.IsAny<string>());            

            _mockCustomerRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(customer);
            _mockCustomerRepository.Setup(x => x.AddAsync(customer)).ReturnsAsync(customer);

            var customerService = new CustomerService(_mockCustomerRepository.Object, _mockAppLogger.Object);

            await Assert.ThrowsAsync<ApplicationException>(async () =>
                await customerService.Create(new Models.CustomerModel { Id = customer.Id, FirstName = customer.FirstName }));
        }
    }
}
