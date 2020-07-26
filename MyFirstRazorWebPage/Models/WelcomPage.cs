using System;
using System.ComponentModel.DataAnnotations;

namespace MyFirstRazorWebPage.Models
{
    public class WelcomPage
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Date Update")]
        public string DateUpdate { get; set; }


        [Required]
        [Display(Name = "Welcome Message")]
        public string Message { get; set; }

        //this is the Welcome Page model. I wish to save in db later. 
    }
}
