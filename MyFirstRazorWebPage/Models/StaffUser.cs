using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace MyFirstRazorWebPage.Models
{
    public class StaffUser
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Staff No")]
        public string StaffNo { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = " Staff Username")]
        public string StaffUserName { get; set; }

        [Required]
        [Display(Name = "Staff Password")]
        public string StaffPassword { get; set; }
    }
}
