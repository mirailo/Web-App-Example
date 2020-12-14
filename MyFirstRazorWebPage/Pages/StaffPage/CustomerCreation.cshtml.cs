using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyFirstRazorWebPage.Models;

namespace MyFirstRazorWebPage.Pages.StaffPage
{

    public class CustomerCreationModel : PageModel
    {
        private readonly HairSalonApptContext _context;

        public CustomerCreationModel(HairSalonApptContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Customer Customer { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Customer.Add(Customer);
            await _context.SaveChangesAsync();

            return RedirectToPage("/StaffPage/CustomerDetails");
        }
    }
    

}
