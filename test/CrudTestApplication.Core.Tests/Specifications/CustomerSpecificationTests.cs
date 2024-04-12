using CrudTestApplication.Core.Specifications;
using CrudTestApplication.Core.Tests.Builders;
using System.Linq;
using Xunit;

namespace CrudTestApplication.Core.Tests.Specifications
{
    public class CustomerSpecificationTests
    {
        private CustomerBuilder CustomerBuilder { get; } = new CustomerBuilder();

        [Fact]
        public void Matches_Customer_With_Category_Spec()
        {
            var spec = new CustomerWithCategorySpecification(CustomerBuilder.CustomerName1);

            var result = CustomerBuilder.GetCustomerCollection()
                .AsQueryable()
                .FirstOrDefault(spec.Criteria);

            Assert.NotNull(result);
            Assert.Equal(CustomerBuilder.CustomerId1, result.Id);
        }
    }
}
