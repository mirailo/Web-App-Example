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
    public class DeregisterModuleModel : PageModel
    {

        [BindProperty]
        public string UserName { get; set; }
        public const string SessionKeyName1 = "username";

        [BindProperty]
        public string UserEmail { get; set; }
        public const string SessionKeyName2 = "email";

        [BindProperty]
        public List<Modules> ModRecords { get; set; } = new List<Modules>();

        [BindProperty]
        public List<bool> IsSelect { get; set; } = new List<bool>();

        [BindProperty]
        public List<Modules> DeRegMod { get; set; } = new List<Modules>();


        public IActionResult OnGet()
        {

            UserName = HttpContext.Session.GetString(SessionKeyName1);
            UserEmail = HttpContext.Session.GetString(SessionKeyName2);

            Console.WriteLine("Current session: " + UserName);
            if (string.IsNullOrEmpty(UserName))
            {
                Console.WriteLine("Session ended");
                return RedirectToPage("/UserLoggedIn/SuccessLogIn");
            }
            else
            {
                Console.WriteLine("Retrieving modules");

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

                List<String> GetRegMod = new List<string>(); //to get module that registered by the student

                while (reader.Read())
                {
                    GetRegMod.Add(reader.GetString(0));
                }

                for (int i = 0; i < GetRegMod.Count; i++)
                {
                    var ModCode = GetRegMod[i];
                    var selectCmd2 = connection.CreateCommand();

                    selectCmd2.CommandText = @"SELECT ModName FROM Modules WHERE ModCode=$modCode ORDER BY ModCode";
                    selectCmd2.Parameters.AddWithValue("$modCode", ModCode);
                    var reader2 = selectCmd2.ExecuteReader();

                    while (reader2.Read())
                    {
                        Modules rec = new Modules();

                        rec.ModCode = GetRegMod[i];
                        rec.ModName = reader2.GetString(0);
                        ModRecords.Add(rec);
                    }
                    IsSelect.Add(false);
                }



                return Page();
            }
        }

        public IActionResult OnPost()
        {


            Console.WriteLine("De-Registering Module");

            for (int i = 0; i < ModRecords.Count(); i++)
            {
                if (IsSelect[i] == true)
                {
                    Console.WriteLine(ModRecords[i].ModCode);
                    DeRegMod.Add(ModRecords[i]);
                }
            }

            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            DatabaseConnect DBCon = new DatabaseConnect(); // your own class and method in DatabaseConnection folder
            string dbStringConnection = DBCon.DBStringConnection();

            connectionStringBuilder.DataSource = dbStringConnection;
            var connection = new SqliteConnection(connectionStringBuilder.ConnectionString);

            connection.Open();



            for (int i=0; i< DeRegMod.Count; i++)
            {
                var selectCmd = connection.CreateCommand();
                selectCmd.CommandText = @"DELETE FROM RegisteredModule WHERE StudenEmail=$email AND ModCode=$modCode";
                selectCmd.Parameters.AddWithValue("$email", UserEmail);
                selectCmd.Parameters.AddWithValue("$modCode", DeRegMod[i].ModCode);
                selectCmd.Prepare();
                selectCmd.ExecuteNonQuery();
            }
           
            return RedirectToPage("/UserModules/ViewRegisteredModule");

        }

    }
}
