using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudTestApplication.Web.ViewModels
{
    public class CustomerViewModel : BaseViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }

    }
}
