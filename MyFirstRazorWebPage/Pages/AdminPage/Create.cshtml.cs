using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyFirstRazorWebPage.Models;

namespace MyFirstRazorWebPage.Pages.AdminPage
{
    public class CreateModel : PageModel
    {
        private readonly RazorPagesMovieContext _context;

        public CreateModel(RazorPagesMovieContext context)
        {
            _context = context;
        }

        public string UserName;
        public const string SessionKeyName = "username";

        public IActionResult OnGet()
        {
            UserName = HttpContext.Session.GetString(SessionKeyName);
            Console.WriteLine("Current session: " + UserName);
            if (string.IsNullOrEmpty(UserName))
            {
                Console.WriteLine("Session ended");
                return RedirectToPage("/AdminPage/Index2");
            }
            else
            {
                return Page();
            }
        }

        [BindProperty]
        public AdminUser AdminUser { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.AdminUser.Add(AdminUser);
            await _context.SaveChangesAsync();

           


            return RedirectToPage("./Index");
        }
    }
}
