using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyFirstRazorWebPage.Pages.StaffPage
{
    public class StaffLogoutModel : PageModel
    {
        public string UserName;
        public const string SessionKeyName1 = "username";

        public string UserEmail;
        public const string SessionKeyName2 = "email";

        public void OnGet()
        {
            UserName = HttpContext.Session.GetString(SessionKeyName1);
            UserEmail = HttpContext.Session.GetString(SessionKeyName2);

            //HttpContext.Session.Remove(UserName);
            HttpContext.Session.Clear();

            //HttpContext.Session.Remove(UserEmail);
            Page();
        }
    }
}
