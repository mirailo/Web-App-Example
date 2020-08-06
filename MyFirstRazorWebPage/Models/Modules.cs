using System;
using System.ComponentModel.DataAnnotations;

namespace MyFirstRazorWebPage.Models
{
    public class Modules
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Module Code")]
        public string ModCode { get; set; }

        [Required]
        [Display(Name = "Module Name")]
        public string ModName { get; set; }

        [Required]
        [Display(Name = "Module Level")]
        public int ModLevel { get; set; }

        [Required]
        [Display(Name = "Module Semester")]
        public int ModSemester { get; set; }
    }
}
