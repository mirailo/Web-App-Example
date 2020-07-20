using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyFirstRazorWebPage.Models;
using MyFirstRazorWebPage.Pages.UserLoggedIn;

namespace MyFirstRazorWebPage.Pages.AdminPage
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesMovieContext _context;


        public IndexModel(RazorPagesMovieContext context)
        {
            _context = context;
        }


        public string UserName;

        public IList<AdminUser> AdminUser { get; set; }

        public async Task OnGetAsync()
        {
            AdminUser = await _context.AdminUser.ToListAsync();
            UserName = HttpContext.Session.GetString("username");
        }

    }
}
