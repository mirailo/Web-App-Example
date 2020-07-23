using System;
using System.ComponentModel.DataAnnotations;

namespace MyFirstRazorWebPage.Models
{
    public class Module
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Module Code")]
        public string MCode { get; set; }

        [Required]
        [Display(Name = "Module Name")]
        public string MName { get; set; }

        [Required]
        [Display(Name = "Level")]
        public string Level { get; set; }
    }
}
