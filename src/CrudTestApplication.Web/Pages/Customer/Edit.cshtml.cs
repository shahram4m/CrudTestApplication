using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CrudTestApplication.Core.Entities;
using CrudTestApplication.Infrastructure.Data;
using CrudTestApplication.Web.ViewModels;
using CrudTestApplication.Web.Interfaces;

namespace CrudTestApplication.Web.Pages.Customer
{
    public class EditModel : PageModel
    {
        private readonly ICustomerPageService _customerPageService;

        public EditModel(ICustomerPageService customerPageService)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            try
            {
                await _customerPageService.UpdateCustomer(Customer);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(Customer.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToPage("./Index");
        }

        private bool CustomerExists(int id)
        {
            var customer = _customerPageService.GetCustomerById(id);
            return customer != null;            
        }
    }
}
