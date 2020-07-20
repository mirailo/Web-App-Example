using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MyFirstRazorWebPage.Models;

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


        public void OnGet()
        {

            UserName = HttpContext.Session.GetString("username");
           
        }



    }
}
