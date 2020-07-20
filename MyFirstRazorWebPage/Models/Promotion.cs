using System;
using System.ComponentModel.DataAnnotations;

namespace MyFirstRazorWebPage.Models
{
    public class Promotion
    {
        public int ID { get; set; }

        public string PromoCode { get; set; }

        public int PromoDuration { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

    }
}
