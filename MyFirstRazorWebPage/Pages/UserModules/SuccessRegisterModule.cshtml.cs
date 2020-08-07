using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyFirstRazorWebPage.Models;

namespace MyFirstRazorWebPage.Pages.UserModules
{
    public class SuccessRegisterModuleModel : PageModel
    {
        public List<Modules> RegMod { get; set; } = new List<Modules>();

   
        public async Task<IActionResult> OnGetAsync(List<Modules> GetRegMod)
        {

            //RegMod = ModRecords;
            Console.WriteLine("Reg Module : "+ GetRegMod.Count());
            return Page();
        }
    }
}
