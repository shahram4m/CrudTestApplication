using CrudTestApplication.Core.Entities;
using System.Linq;
using Xunit;

namespace CrudTestApplication.Core.Tests.Entities
{
    public class CategoryTests
    {
        private int _testCustomerId = 3;
        private int _testCategoryId = 5;
        private string _testCustomerName = "Reason";

        //[Fact]
        //public void Adds_Customer_Into_Category()
        //{
        //    var category = Category.Create(_testCategoryId, "newCategory");
        //    category.AddCustomer(_testCustomerId, _testCustomerName);

        //    var firstItem = category.Customers.Single();
        //    Assert.Equal(_testCategoryId, category.Id);
        //    Assert.Equal(_testCustomerId, firstItem.Id);
        //}
    }
}
