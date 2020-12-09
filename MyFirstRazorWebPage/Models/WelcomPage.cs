using System;
using System.ComponentModel.DataAnnotations;

namespace MyFirstRazorWebPage.Models
{
    public class WelcomePage
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Date Update")]
        public string DateUpdate { get; set; }


        [Required]
        [Display(Name = "Welcome Message")]
        public string Message { get; set; }

        
    }
}
