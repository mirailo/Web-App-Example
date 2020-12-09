using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace MyFirstRazorWebPage.Models
{
    public class AdminUser
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
        public string AdminUserName { get; set; }

        [Required]
        [Display(Name = "Staff Password")]
        public string AdminPassword { get; set; }
    }
}
