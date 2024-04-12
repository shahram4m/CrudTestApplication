using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CrudTestApplication.Core.Entities;
using CrudTestApplication.Infrastructure.Data;
using CrudTestApplication.Web.Interfaces;
using CrudTestApplication.Web.ViewModels;

namespace CrudTestApplication.Web.Pages.Customer
{
    public class CreateModel : PageModel
    {
        private readonly ICustomerPageService _customerPageService;

        public CreateModel(ICustomerPageService customerPageService)
        {
            _customerPageService = customerPageService ?? throw new ArgumentNullException(nameof(customerPageService));
        }


        [BindProperty]
        public CustomerViewModel Customer { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Customer = await _customerPageService.CreateCustomer(Customer);
            return RedirectToPage("./Index");
        }
    }
}