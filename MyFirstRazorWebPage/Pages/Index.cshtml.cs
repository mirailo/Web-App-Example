using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MyFirstRazorWebPage.Models;

namespace MyFirstRazorWebPage.Pages
{
    public class IndexModel : PageModel
    {

        private readonly ILogger<IndexModel> _logger;
        private readonly HairSalonApptContext _context;
        private readonly IWebHostEnvironment _env;


        public IndexModel(ILogger<IndexModel> logger, HairSalonApptContext context, IWebHostEnvironment env)
        {
            _logger = logger;
            _context = context;
            _env = env;
        }

        public IActionResult OnGet()
        {

            return Page();

        }
    }
}
