using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CrudTestApplication.Core.Entities;
using CrudTestApplication.Infrastructure.Data;
using CrudTestApplication.Web.Interfaces;
using CrudTestApplication.Web.ViewModels;

namespace CrudTestApplication.Web.Pages.Customer
{
    public class DeleteModel : PageModel
    {
        private readonly ICustomerPageService _customerPageService;

        public DeleteModel(ICustomerPageService customerPageService)
        {
            _customerPageService = customerPageService ?? throw new ArgumentNullException(nameof(customerPageService));
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? customerId)
        {
            if (customerId == null)
            {
                return NotFound();
            }

            await _customerPageService.DeleteCustomer(Customer);          
            return RedirectToPage("./Index");
        }
    }
}
