using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MyFirstRazorWebPage.Models;

namespace MyFirstRazorWebPage.Pages.UserModules
{
    public class UserProfileModel : PageModel
    {
        private readonly RazorPagesMovieContext _context;

        public UserProfileModel(RazorPagesMovieContext context)
        {
            _context = context;
        }

        public User User { get; set; }

        public string UserName;
        public const string SessionKeyName1 = "username";

        public string UserEmail;
        public const string SessionKeyName2 = "email";

        public string Pwd { get; set; }
        public string FName { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            UserName = HttpContext.Session.GetString(SessionKeyName1);
            UserEmail = HttpContext.Session.GetString(SessionKeyName2);

            Console.WriteLine("Current session: " + UserName);
            if (string.IsNullOrEmpty(UserName))
            {
                Console.WriteLine("Session ended");
                return RedirectToPage("/Users/UserLogin");
            }
            else
            {
                var connectionStringBuilder = new SqliteConnectionStringBuilder();
                connectionStringBuilder.DataSource = "/Users/zairulmazwan/Projects/Web-App-Example/MyFirstRazorWebPage/RazorPagesMovieContext-4626ba78-c68f-4200-bc79-dd49c8d85ee3.db";
                var connection = new SqliteConnection(connectionStringBuilder.ConnectionString);

                connection.Open();

                var selectCmd = connection.CreateCommand();
                selectCmd.CommandText = @"SELECT FirstName, Password FROM User WHERE EmailAdd=$EmailAdd";
                selectCmd.Parameters.AddWithValue("$EmailAdd", UserEmail);

                var reader = selectCmd.ExecuteReader();
               
                while (reader.Read())
                {
                    FName = reader.GetString(0);
                    Pwd = reader.GetString(1);
                }

                Console.WriteLine("Retrieved first name : "+ FName);
                Console.WriteLine("Retrieved password : " + Pwd);
                return Page();
            }

        }
    }
}
