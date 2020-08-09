using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using MyFirstRazorWebPage.Models;
using MyFirstRazorWebPage.Pages.DatabaseConnection;

namespace MyFirstRazorWebPage.Pages.UserModules
{
    public class ViewRegisteredModuleModel : PageModel
    {
        [BindProperty]
        public List<Modules> Modules { get; set; } = new List<Modules>();

        [BindProperty]
        public List<string> ModCode { get; set; } = new List<string>();

        [BindProperty]
        public User User { get; set; }


        [BindProperty]
        public string UserName { get; set; }
        public const string SessionKeyName1 = "username";

        [BindProperty]
        public string UserEmail { get; set; }
        public const string SessionKeyName2 = "email";

        public IActionResult OnGet()
        {

            UserName = HttpContext.Session.GetString(SessionKeyName1);
            UserEmail = HttpContext.Session.GetString(SessionKeyName2);
            Console.WriteLine("Current session: " + UserName);


            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            
            DatabaseConnect DBCon = new DatabaseConnect(); // your own class and method in DatabaseConnection folder
            string dbStringConnection = DBCon.DBStringConnection();

            connectionStringBuilder.DataSource = dbStringConnection;
            var connection = new SqliteConnection(connectionStringBuilder.ConnectionString);

            connection.Open();

            var selectCmd = connection.CreateCommand();
            selectCmd.CommandText = @"SELECT ModCode FROM RegisteredModule WHERE StudenEmail=$email ORDER BY ModCode";
            selectCmd.Parameters.AddWithValue("$email", UserEmail);
            var reader = selectCmd.ExecuteReader();

            while (reader.Read())
            {
              
              
                string modCode = reader.GetString(0); //temporary variable used to get the module codes
                Console.WriteLine("Module found : "+ modCode);
                ModCode.Add(modCode); //keep it to the list for future use
            }

            //for loop is used because student might have more than 1 module registered
            for (int i=0; i< ModCode.Count; i++)
            {
                var selectCmd2 = connection.CreateCommand();
                selectCmd2.CommandText = @"SELECT * FROM Modules WHERE ModCode=$modCode ORDER BY ModCode";
                selectCmd2.Parameters.AddWithValue("$modCode", ModCode[i]);
                var reader2 = selectCmd2.ExecuteReader();

                while (reader2.Read())
                {

                    Modules mod = new Modules(); //temporary variable used to hold the record found
                    mod.ModCode = reader2.GetString(1); //start from 1 because we dont want ID field from the db
                    mod.ModName = reader2.GetString(2);
                    mod.ModLevel = reader2.GetInt32(3);
                    mod.ModSemester = reader2.GetInt32(4);
                    Modules.Add(mod); //the record now saved to the global variable
                }
            }

            return Page();

        }
    }
}
