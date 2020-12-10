using System;
using System.ComponentModel.DataAnnotations;

namespace MyFirstRazorWebPage.Models
{
    public class Picture
    {

        public int Id { get; set; }

        [Required]
        public string PicName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }
    }
}