using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CrudTestApplication.Web.Tests.Pages
{
    public class CustomerPageTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        public HttpClient Client { get; }

        public CustomerPageTests(CustomWebApplicationFactory<Startup> factory)
        {
            Client = factory.CreateClient();
        }

        [Fact]
        public async Task Customer_Page_Test()
        {
            // Arrange & Act
            var response = await Client.GetAsync("/Customer");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Contains("Samsung", stringResponse);
        }
    }
}
