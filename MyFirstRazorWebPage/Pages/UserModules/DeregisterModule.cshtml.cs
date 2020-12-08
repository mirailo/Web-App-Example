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

namespace MyFirstRazorWebPage.Pages.UserServices
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
        public List<Services> ModRecords { get; set; } = new List<Services>();

        [BindProperty]
        public List<bool> IsSelect { get; set; } = new List<bool>();

        [BindProperty]
        public List<Services> DeRegMod { get; set; } = new List<Services>();


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
                Console.WriteLine("Retrieving Services");

                var connectionStringBuilder = new SqliteConnectionStringBuilder();
                DatabaseConnect DBCon = new DatabaseConnect(); // your own class and method in DatabaseConnection folder
                string dbStringConnection = DBCon.DBStringConnection();

                connectionStringBuilder.DataSource = dbStringConnection;
                var connection = new SqliteConnection(connectionStringBuilder.ConnectionString);

                connection.Open();

                var selectCmd = connection.CreateCommand();

                selectCmd.CommandText = @"SELECT ServiceCode FROM RegisteredModule WHERE StudenEmail=$email ORDER BY ServiceCode";
                selectCmd.Parameters.AddWithValue("$email", UserEmail);
                var reader = selectCmd.ExecuteReader();

                List<String> GetRegMod = new List<string>(); //to get module that registered by the student

                while (reader.Read())
                {
                    GetRegMod.Add(reader.GetString(0));
                }

                for (int i = 0; i < GetRegMod.Count; i++)
                {
                    var ServiceCode = GetRegMod[i];
                    var selectCmd2 = connection.CreateCommand();

                    selectCmd2.CommandText = @"SELECT ServiceName FROM Services WHERE ServiceCode=$ServiceCode ORDER BY ServiceCode";
                    selectCmd2.Parameters.AddWithValue("$ServiceCode", ServiceCode);
                    var reader2 = selectCmd2.ExecuteReader();

                    while (reader2.Read())
                    {
                        Services rec = new Services();

                        rec.ServiceCode = GetRegMod[i];
                        rec.ServiceName = reader2.GetString(0);
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
                    Console.WriteLine(ModRecords[i].ServiceCode);
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
                selectCmd.CommandText = @"DELETE FROM RegisteredModule WHERE StudenEmail=$email AND ServiceCode=$ServiceCode";
                selectCmd.Parameters.AddWithValue("$email", UserEmail);
                selectCmd.Parameters.AddWithValue("$ServiceCode", DeRegMod[i].ServiceCode);
                selectCmd.Prepare();
                selectCmd.ExecuteNonQuery();
            }
           
            return RedirectToPage("/UserServices/ViewRegisteredModule");

        }

    }
}
