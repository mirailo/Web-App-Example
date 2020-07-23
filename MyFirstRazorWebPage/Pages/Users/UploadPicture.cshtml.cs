using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyFirstRazorWebPage.Models;

namespace MyFirstRazorWebPage.Pages.Users
{
    public class UploadPictureModel : PageModel
    {

        private readonly RazorPagesMovieContext _context;


        public UploadPictureModel(RazorPagesMovieContext context)
        {
            _context = context;
        }

        [BindProperty]
        public IList<User> Users { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Users = await _context.User.ToListAsync();

            return Page();

        }
    }
}
