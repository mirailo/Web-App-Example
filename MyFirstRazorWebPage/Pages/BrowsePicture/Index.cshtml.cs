using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyFirstRazorWebPage.Pages.BrowsePicture
{
    public class IndexModel : PageModel
    {

        public string PicPath { get; set; }

        public IActionResult OnPost()
        {
            
            return Page();
        }
    }
}
