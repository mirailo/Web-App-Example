using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyFirstRazorWebPage.Models;

namespace MyFirstRazorWebPage.Pages.StaffPage
{
    public class DetailsModel : PageModel
    {
        private readonly HairSalonApptContext _context;

        public DetailsModel(HairSalonApptContext context)
        {
            _context = context;
        }

        public StaffUser StaffUser { get; set; }
        public string UserName;
        public const string SessionKeyName = "username";

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            StaffUser = await _context.StaffUser.FirstOrDefaultAsync(m => m.ID == id);

            if (StaffUser == null)
            {
                return NotFound();
            }

            UserName = HttpContext.Session.GetString(SessionKeyName);
            Console.WriteLine("Current session: " + UserName);
            if (string.IsNullOrEmpty(UserName))
            {
                Console.WriteLine("Session ended");
                return RedirectToPage("/StaffPage/Index2");
            }
            else
            {
                return Page();
            }

           
        }
    }
}
