using CrudTestApplication.Application.Models.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CrudTestApplication.Application.Models
{
    public class CustomerModel : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        [Remote("CheckCorrectBankAccountNumber", "Customer", ErrorMessage = "Remote validation is working")]
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        //[Remote("CheckCorrectBankAccountNumber", "Customer")]
        public string BankAccountNumber { get; set; }
    }
}
