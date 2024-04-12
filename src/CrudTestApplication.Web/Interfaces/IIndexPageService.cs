using CrudTestApplication.Web.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrudTestApplication.Web.Interfaces
{
    // NOTE : This is the whole page service, it could be include all categories and customers
    // this is the razor page based service
    public interface IIndexPageService
    {
        Task<IEnumerable<CustomerViewModel>> GetCustomers();        
    }
}
