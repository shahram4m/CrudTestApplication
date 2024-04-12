using CrudTestApplication.Core.Entities;
using CrudTestApplication.Core.Specifications.Base;

namespace CrudTestApplication.Core.Specifications
{
    public class CustomerWithCategorySpecification : BaseSpecification<Customer>
    {
        public CustomerWithCategorySpecification(string customerName) 
            : base(p => p.FirstName.ToLower().Contains(customerName.ToLower()))
        {
            AddInclude(p => p);
        }

        public CustomerWithCategorySpecification() : base(null)
        {
            AddInclude(p => p);
        }
    }
}
