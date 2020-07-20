using System;
using System.ComponentModel.DataAnnotations;

namespace MyFirstRazorWebPage.Models
{
    public class AdminUser
    {
        public int ID { get; set; }

        [Required]
        public string StaffNo { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Admin User Name")]
        public string AdminUserName { get; set; }

        [Required]
        [Display(Name = "Admin Password")]
        public string AdminPassword { get; set; }
    }
}
