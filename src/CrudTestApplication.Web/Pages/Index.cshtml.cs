using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrudTestApplication.Web.Interfaces;
using CrudTestApplication.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CrudTestApplication.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IIndexPageService _indexPageService;

        public IndexModel(IIndexPageService indexPageService)
        {
            _indexPageService = indexPageService ?? throw new ArgumentNullException(nameof(indexPageService));
        }

        public IEnumerable<CustomerViewModel> CustomerList { get; set; } = new List<CustomerViewModel>();

        public async Task<IActionResult> OnGet()
        {
            CustomerList = await _indexPageService.GetCustomers();

            //CategoryModel = await _indexPageService.GetCategoryWithCustomers(1);
            //CustomerModel = await _indexPageService.GetCustomers();
            return Page();
        }
    }
}
