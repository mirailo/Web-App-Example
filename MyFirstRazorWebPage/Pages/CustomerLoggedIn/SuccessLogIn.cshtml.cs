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

namespace MyFirstRazorWebPage.Pages.CustomerLoggedIn
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

        [BindProperty]
        public string pathPicture { get; set; }

        [BindProperty]
        public string FileName { get; set; }

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
                return RedirectToPage("/Customers/CustomerLogin");

            }
            else
            {
                var connectionStringBuilder = new SqliteConnectionStringBuilder();
                DatabaseConnect DBCon = new DatabaseConnect();
                string dbStringConnection = DBCon.DBStringConnection(); //getting the connection string from this class


                connectionStringBuilder.DataSource = dbStringConnection;
                var connection = new SqliteConnection(connectionStringBuilder.ConnectionString);

                connection.Open();

                var selectCmd = connection.CreateCommand();
                selectCmd.CommandText = @"SELECT PicName FROM Picture WHERE Email=$email";
                selectCmd.Parameters.AddWithValue("$email", UserEmail);

                var reader = selectCmd.ExecuteReader();
                var fileName = "";

                while (reader.Read())
                {
                    fileName = reader.GetString(0);
                }

                if (string.IsNullOrEmpty(fileName))
                {
                    pathPicture = "DefaultPic.png";
                    Console.WriteLine("Default pic : " + pathPicture);
                    return Page();
                }

                pathPicture = fileName;

                Console.WriteLine("File name is : " + fileName);
                pathPicture = fileName;

                return Page();
            }

        }


    }
}
