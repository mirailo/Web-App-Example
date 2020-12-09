using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using MyFirstRazorWebPage.Models;
using MyFirstRazorWebPage.Pages.DatabaseConnection;

namespace MyFirstRazorWebPage.Pages.UserLoggedIn
{
    public class SuccessLogInModel : PageModel
    {
        private readonly ILogger<SuccessLogInModel> _logger;

        public SuccessLogInModel(ILogger<SuccessLogInModel> logger)
        {
            _logger = logger;
        }

        public string UserName;
        public const string SessionKeyName1 = "username";

        
        public string UserEmail;
        public const string SessionKeyName2 = "email";

        public string SessionID;
        public const string SessionKeyName3 = "sessionID";


        public IActionResult OnGet()
        {

            UserName = HttpContext.Session.GetString(SessionKeyName1);
            UserEmail = HttpContext.Session.GetString(SessionKeyName2);
            SessionID = HttpContext.Session.GetString(SessionKeyName3);

            
            Console.WriteLine("Current session: " + UserName);
            Console.WriteLine("Current session ID: " + SessionID);

            if (string.IsNullOrEmpty(UserName))
            {
                Console.WriteLine("Session ended");
                return RedirectToPage("/Customers/UserLogin");

            }
            else
            {

                return Page();
                
            }

        }


    }
}
