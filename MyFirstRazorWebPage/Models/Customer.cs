using System;
using System.ComponentModel.DataAnnotations;

namespace MyFirstRazorWebPage.Models
{
    public class Customer
    {
        public int ID { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string EmailAdd { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }

    }
}
