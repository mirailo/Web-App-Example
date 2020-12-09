using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyFirstRazorWebPage.Models;


namespace MyFirstRazorWebPage.Pages.Customers
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesMovieContext _context;

        public IndexModel(RazorPagesMovieContext context)
        {
            _context = context;
        }

        public string UserName;
        public const string SessionKeyName = "username";
       

        public IList<User> User { get;set; }

        public async Task<IActionResult> OnGetAsync()
        {
            User = await _context.User.ToListAsync();

            try
            {
                Console.WriteLine("Hello Session2");
                UserName = JsonSerializer.Deserialize<string>(HttpContext.Session.GetString(SessionKeyName));
              
                return Page();
            }
            catch
            {
                return RedirectToPage("/Users/UserLogin");
                throw;
            }
        }
    }
}
