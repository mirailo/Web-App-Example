using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyFirstRazorWebPage.Models;

namespace MyFirstRazorWebPage.Pages.ModuleManagement
{
    public class CreateModel : PageModel
    {
        private readonly RazorPagesMovieContext _context;

        public CreateModel(RazorPagesMovieContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Services Services { get; set; }


       
        public int[] Time = { 15, 30, 45, 60, 75, 90, 120 };


        public double[] Price = { 5 , 7.50, 10, 12.50, 15, 17.50, 20, 25, 30, 35, 40, 50 };

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Services.Add(Services);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
