using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CrudTestApplication.Web.Interfaces;
using CrudTestApplication.Web.ViewModels;

namespace CrudTestApplication.Web.Pages.Customer
{
    public class DetailsModel : PageModel
    {
        private readonly ICustomerPageService _customerPageService;

        public DetailsModel(ICustomerPageService customerPageService)
        {
            _customerPageService = customerPageService ?? throw new ArgumentNullException(nameof(customerPageService));
        }       

        public CustomerViewModel Customer { get; set; }

        public async Task<IActionResult> OnGetAsync(int? customerId)
        {
            if (customerId == null)
            {
                return NotFound();
            }

            Customer = await _customerPageService.GetCustomerById(customerId.Value);
            if (Customer == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
