using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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
        private readonly HairSalonApptContext _context;


        public IndexModel(HairSalonApptContext context)
        {
            _context = context;
        }


        public string UserName;
        public const string SessionKeyName = "username";

        public string SessionID;
        public const string SessionKeyID = "sessionID";

        public IList<AdminUser> AdminUser { get; set; }

      
        public async Task<IActionResult> OnGetAsync()
        {
            AdminUser = await _context.AdminUser.ToListAsync();

                Console.WriteLine("Hello Session2");
                //UserName = JsonSerializer.Deserialize<string>(HttpContext.Session.GetString(SessionKeyName));
                UserName = HttpContext.Session.GetString(SessionKeyName);
                SessionID = HttpContext.Session.GetString(SessionKeyID);

                //UserName = HttpContext.Request.Cookies[SessionKeyName];

                Console.WriteLine("User session : "+UserName);
                Console.WriteLine("User session ID : "+ SessionID);


            if (string.IsNullOrEmpty(UserName))
                {
                    return RedirectToPage("/AdminPage/Index2");
                }
                else
                {
                    return Page();
                }
        }

        
    }
}
