using System;
using System.ComponentModel.DataAnnotations;

namespace MyFirstRazorWebPage.Models
{
    public class WelcomePage
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Message Paragraph 1:")]
        public string Message1 { get; set; }

        [Required]
        [Display(Name = "Message Paragraph 2:")]
        public string Message2 { get; set; }

        [Required]
        [Display(Name = "Message Paragraph 3:")]
        public string Message3 { get; set; }


        [Required]
        [Display(Name = "Date Update")]
        public string DateUpdate { get; set; }

        [Required]
        [Display(Name = "User")]
        public string User { get; set; }
        
    }
}
