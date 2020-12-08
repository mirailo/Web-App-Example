using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyFirstRazorWebPage.Models;

namespace MyFirstRazorWebPage.Pages.UserServices
{
    public class SuccessRegisterModuleModel : PageModel
    {
        public List<Services> RegMod { get; set; } = new List<Services>();

   
        public async Task<IActionResult> OnGetAsync(List<Services> GetRegMod)
        {

            //RegMod = ModRecords;
            Console.WriteLine("Reg Module : "+ GetRegMod.Count());
            return Page();
        }
    }
}
