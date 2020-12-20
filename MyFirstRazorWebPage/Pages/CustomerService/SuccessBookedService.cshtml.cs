using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyFirstRazorWebPage.Models;

namespace MyFirstRazorWebPage.Pages.CustomerService
{
    public class SuccessBookedServiceModel : PageModel
    {
        public List<Services> BookedService { get; set; } = new List<Services>();

   
        public async Task<IActionResult> OnGetAsync(List<Services> GetBookedService)
        {

            //RegMod = ModRecords;
            Console.WriteLine("Booked Services : "+ GetBookedService.Count());
            return Page();
        }
    }
}
