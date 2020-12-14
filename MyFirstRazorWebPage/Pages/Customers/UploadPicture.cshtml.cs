using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyFirstRazorWebPage.Models;

namespace MyFirstRazorWebPage.Pages.Customers
{
    public class UploadPictureModel : PageModel
    {

        private readonly HairSalonApptContext _context;


        public UploadPictureModel(HairSalonApptContext context)
        {
            _context = context;
        }

        [BindProperty]
        public IList<Customer> Users { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Users = await _context.Customer.ToListAsync();

            return Page();

        }
    }
}
