using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using MyFirstRazorWebPage.Pages.DatabaseConnection;

namespace MyFirstRazorWebPage.Pages.UserLoggedIn
{
    public class UserLogoutModel : PageModel
    {
        public string Username;
        public const string SessionKeyName1 = "username";

        public string UserEmail;
        public const string SessionKeyName2 = "email";

        public void OnGet()
        {
            Username = HttpContext.Session.GetString(SessionKeyName1);
            UserEmail = HttpContext.Session.GetString(SessionKeyName2);

            
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            DatabaseConnect DBCon = new DatabaseConnect(); // your own class and method in DatabaseConnection folder
            string dbStringConnection = DBCon.DBStringConnection();

            connectionStringBuilder.DataSource = dbStringConnection;
            var connection = new SqliteConnection(connectionStringBuilder.ConnectionString);
            connection.Open();

            Console.WriteLine("Username : "+Username);

            var selectCmd2 = connection.CreateCommand();
            selectCmd2.CommandText = @"DELETE FROM UserSession WHERE Username=$username";
            selectCmd2.Parameters.AddWithValue("$username", Username);
            selectCmd2.Prepare();
            selectCmd2.ExecuteNonQuery();

            HttpContext.Session.Clear();

            
            Page();
        }
    }
}
