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
    public class ViewBookedServiceModel : PageModel
    {
        [BindProperty]
        public List<Services> Services { get; set; } = new List<Services>();

        [BindProperty]
        public List<string> ServiceCode { get; set; } = new List<string>();

        [BindProperty]
        public Customer Customer { get; set; }


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
            selectCmd.CommandText = @"SELECT ServiceCode FROM BookedService WHERE Email=$email ORDER BY ServiceCode";
            selectCmd.Parameters.AddWithValue("$email", UserEmail);
            var reader = selectCmd.ExecuteReader();

            while (reader.Read())
            {
      
                string serviceCode = reader.GetString(0); //temporary variable used to get the module codes
                Console.WriteLine("Appointment found : "+ ServiceCode);
                ServiceCode.Add(serviceCode); //keep it to the list for future use
            }

            //for loop is used because student might have more than 1 module registered
            for (int i=0; i< ServiceCode.Count; i++)
            {
                var selectCmd2 = connection.CreateCommand();
                selectCmd2.CommandText = @"SELECT * FROM Services WHERE ServiceCode=$ServiceCode ORDER BY ServiceCode";
                selectCmd2.Parameters.AddWithValue("$ServiceCode", ServiceCode[i]);
                var reader2 = selectCmd2.ExecuteReader();

                while (reader2.Read())
                {

                    Services ser = new Services(); //temporary variable used to hold the record found
                    ser.ServiceCode = reader2.GetString(1); //start from 1 because we dont want ID field from the db
                    ser.ServiceName = reader2.GetString(2);
                    ser.ServiceDuration = reader2.GetInt32(3);
                    ser.ServicePrice = reader2.GetInt32(4);
                    Services.Add(ser); //the record now saved to the global variable
                }
            }

            return Page();

        }
    }
}
