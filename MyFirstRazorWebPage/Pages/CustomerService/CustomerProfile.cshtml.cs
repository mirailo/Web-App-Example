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
using MyFirstRazorWebPage.Pages.DatabaseConnection;

namespace MyFirstRazorWebPage.Pages.CustomerService
{
    public class CustomerProfileModel : PageModel
    {
        private readonly RazorPagesMovieContext _context;

        public CustomerProfileModel(RazorPagesMovieContext context)
        {
            _context = context;
        }

        public User User { get; set; }

        public string UserName;
        public const string SessionKeyName1 = "username";

        public string SessionID;
        public const string SessionKeyName3 = "sessionID";

        public string UserEmail;
        public const string SessionKeyName2 = "email";

        public string Pwd { get; set; }
        public string EmailAdd { get; set; }
        public string getUserNameFromSession { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            UserName = HttpContext.Session.GetString(SessionKeyName1);
            UserEmail = HttpContext.Session.GetString(SessionKeyName2);

            SessionID = HttpContext.Session.GetString(SessionKeyName3);


            Console.WriteLine("Current session ID: " + SessionID);
            if (string.IsNullOrEmpty(UserName))
            {
                Console.WriteLine("Session ended");
                return RedirectToPage("/Users/UserLogin");
            }
            else
            {
                

                var connectionStringBuilder = new SqliteConnectionStringBuilder();
                DatabaseConnect DBCon = new DatabaseConnect();
                string dbStringConnection = DBCon.DBStringConnection();

                connectionStringBuilder.DataSource = dbStringConnection;
                var connection = new SqliteConnection(connectionStringBuilder.ConnectionString);

                connection.Open();

                var selectCmd = connection.CreateCommand();
                selectCmd.CommandText = @"SELECT EmailAdd, Password FROM User WHERE FirstName=$userName";
                selectCmd.Parameters.AddWithValue("$userName", UserName);

                var reader = selectCmd.ExecuteReader();
               
                while (reader.Read())
                {
                    EmailAdd = reader.GetString(0);
                    Pwd = reader.GetString(1);
                }

                Console.WriteLine("Retrieved first name : "+ EmailAdd);
                Console.WriteLine("Retrieved password : " + Pwd);
                return Page();
            }

        }
    }
}
