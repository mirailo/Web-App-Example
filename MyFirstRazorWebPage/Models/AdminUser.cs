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
        [Display(Name = "Admin User Name")]
        public string AdminUserName { get; set; }

        [Required]
        [Display(Name = "Admin Password")]
        public string AdminPassword { get; set; }
    }
}
