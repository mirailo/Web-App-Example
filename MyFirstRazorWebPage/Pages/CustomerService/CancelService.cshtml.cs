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

namespace MyFirstRazorWebPage.Pages.CustomerService
{
    public class CancelServiceModel : PageModel
    {

        [BindProperty]
        public string UserName { get; set; }
        public const string SessionKeyName1 = "username";

        [BindProperty]
        public string UserEmail { get; set; }
        public const string SessionKeyName2 = "email";

        [BindProperty]
        public List<Services> ServiceRecords { get; set; } = new List<Services>();

        [BindProperty]
        public List<bool> IsSelect { get; set; } = new List<bool>();

        [BindProperty]
        public List<Services> CancelService{ get; set; } = new List<Services>();


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

                selectCmd.CommandText = @"SELECT ServiceCode FROM BookedService WHERE Email=$email ORDER BY ServiceCode";
                selectCmd.Parameters.AddWithValue("$email", UserEmail);
                var reader = selectCmd.ExecuteReader();

                List<String> GetBookedService = new List<string>(); 

                while (reader.Read())
                {
                    GetBookedService.Add(reader.GetString(0));
                }

                for (int i = 0; i < GetBookedService.Count; i++)
                {
                    var ServiceCode = GetBookedService[i];
                    var selectCmd2 = connection.CreateCommand();

                    selectCmd2.CommandText = @"SELECT ServiceName FROM Services WHERE ServiceCode=$SCode ORDER BY ServiceCode";
                    selectCmd2.Parameters.AddWithValue("$SCode", ServiceCode);
                    var reader2 = selectCmd2.ExecuteReader();

                    while (reader2.Read())
                    {
                        Services rec = new Services();

                        rec.ServiceCode = GetBookedService[i];
                        rec.ServiceName = reader2.GetString(0);
                        ServiceRecords.Add(rec);
                    }
                    IsSelect.Add(false);
                }



                return Page();
            }
        }

        public IActionResult OnPost()
        {


            Console.WriteLine("Cancelling Appointment");

            for (int i = 0; i < ServiceRecords.Count(); i++)
            {
                if (IsSelect[i] == true)
                {
                    Console.WriteLine(ServiceRecords[i].ServiceCode);
                    CancelService.Add(ServiceRecords[i]);
                }
            }

            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            DatabaseConnect DBCon = new DatabaseConnect(); // your own class and method in DatabaseConnection folder
            string dbStringConnection = DBCon.DBStringConnection();

            connectionStringBuilder.DataSource = dbStringConnection;
            var connection = new SqliteConnection(connectionStringBuilder.ConnectionString);

            connection.Open();



            for (int i=0; i< CancelService.Count; i++)
            {
                var selectCmd = connection.CreateCommand();
                selectCmd.CommandText = @"DELETE FROM BookedService WHERE Email=$email AND ServiceCode=$SCode";
                selectCmd.Parameters.AddWithValue("$email", UserEmail);
                selectCmd.Parameters.AddWithValue("$SCode", CancelService[i].ServiceCode);
                selectCmd.Prepare();
                selectCmd.ExecuteNonQuery();
            }
           
            return RedirectToPage("/CustomerService/ViewBookedService");

        }

    }
}
