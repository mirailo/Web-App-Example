using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyFirstRazorWebPage.Models;

namespace MyFirstRazorWebPage.Pages.AdminPage
{
    public class DeleteModel : PageModel
    {
        private readonly RazorPagesMovieContext _context;

        public DeleteModel(RazorPagesMovieContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AdminUser AdminUser { get; set; }

        public string UserName;
        public const string SessionKeyName = "username";

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AdminUser = await _context.AdminUser.FirstOrDefaultAsync(m => m.ID == id);

            if (AdminUser == null)
            {
                return NotFound();
            }

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AdminUser = await _context.AdminUser.FindAsync(id);

            if (AdminUser != null)
            {
                _context.AdminUser.Remove(AdminUser);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
