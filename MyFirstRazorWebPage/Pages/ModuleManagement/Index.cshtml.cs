using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyFirstRazorWebPage.Models;

namespace MyFirstRazorWebPage.Pages.ModuleManagement
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesMovieContext _context;

        public IndexModel(RazorPagesMovieContext context)
        {
            _context = context;
        }

        public string UserName;
        public const string SessionKeyName1 = "username";

        public IList<Modules> Modules { get;set; }

        public async Task OnGetAsync()
        {
            Modules = await _context.Modules.ToListAsync();
            UserName = HttpContext.Session.GetString(SessionKeyName1);
            UserName = HttpContext.Session.Id;
        }
    }
}
