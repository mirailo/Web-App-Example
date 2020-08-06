using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyFirstRazorWebPage.Models;

namespace MyFirstRazorWebPage.Pages.ModuleManagement
{
    public class DeleteModel : PageModel
    {
        private readonly RazorPagesMovieContext _context;

        public DeleteModel(RazorPagesMovieContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Modules Modules { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Console.WriteLine("Delete Page!");
            if (id == null)
            {
                return NotFound();
            }

            Modules = await _context.Modules.FirstOrDefaultAsync(m => m.ID == id);

            if (Modules == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Modules = await _context.Modules.FindAsync(id);

            if (Modules != null)
            {
                _context.Modules.Remove(Modules);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
