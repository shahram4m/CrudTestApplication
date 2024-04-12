using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CrudTestApplication.Web.ViewModels;
using CrudTestApplication.Web.Interfaces;

namespace CrudTestApplication.Web.Pages.Customer
{
    public class IndexModel : PageModel
    {
        private readonly ICustomerPageService _customerPageService;

        public IndexModel(ICustomerPageService customerPageService)
        {
            _customerPageService = customerPageService ?? throw new ArgumentNullException(nameof(customerPageService));
        }

        public IEnumerable<CustomerViewModel> CustomerList { get; set; } = new List<CustomerViewModel>();

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            CustomerList = await _customerPageService.GetCustomers(SearchTerm);
            return Page();
        }
    }
}
