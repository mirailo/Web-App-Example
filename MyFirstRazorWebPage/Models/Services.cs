using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstRazorWebPage.Models
{
    public class Services
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Service Code")]
        public string ServiceCode { get; set; }

        [Required]
        [Display(Name = "Service Name")]
        public string ServiceName { get; set; }

        [Required]
        [Display(Name = "Service Duration")]
        public int ServiceDuration { get; set; }

        [Required]
        [Display(Name = "Service Price")]
        public int ServicePrice { get; set; }



    }
}
