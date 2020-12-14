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

    public class BookServiceModel : PageModel
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
        public List<Services> GetBookedServices { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            UserName = HttpContext.Session.GetString(SessionKeyName1);
            UserEmail = HttpContext.Session.GetString(SessionKeyName2);

            Console.WriteLine("Current session: " + UserName);
            if (string.IsNullOrEmpty(UserName))
            {
                Console.WriteLine("Session ended");
                return RedirectToPage("/CustomerLoggedIn/SuccessLogIn");
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

                selectCmd.CommandText = @"SELECT * FROM Services ORDER BY ServiceDuration";
                var reader = selectCmd.ExecuteReader();

                while (reader.Read())
                {

                    Services ser = new Services();
                    ser.ServiceCode = reader.GetString(1);
                    ser.ServiceName = reader.GetString(2);
                    ser.ServiceDuration = reader.GetInt32(3);
                    ser.ServicePrice = reader.GetInt32(4);

                    ServiceRecords.Add(ser);
                    IsSelect.Add(false);

                }
                connection.Close();
                return Page();
            }

        }

        public async Task<IActionResult> OnPostAsync()
        {
            Console.WriteLine("Booking Service:");

            for (int i = 0; i < ServiceRecords.Count(); i++)
            {
                if (IsSelect[i] == true)
                {
                    Console.WriteLine(ServiceRecords[i].ServiceName);
                    GetBookedServices.Add(ServiceRecords[i]);
                }
            }

            Console.WriteLine("Booked Service/s : " + GetBookedServices.Count());

            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            DatabaseConnect DBCon = new DatabaseConnect(); // your own class and method in DatabaseConnection folder
            string dbStringConnection = DBCon.DBStringConnection();

            connectionStringBuilder.DataSource = dbStringConnection;
            var connection = new SqliteConnection(connectionStringBuilder.ConnectionString);

            connection.Open();

            var selectCmd = connection.CreateCommand();
            selectCmd.CommandText = @"SELECT ServiceCode FROM BookedService WHERE Email=$email";
            Console.WriteLine("Email : " + UserEmail);
            selectCmd.Parameters.AddWithValue("$email", UserEmail);
            var reader = selectCmd.ExecuteReader();

            List<string> CheckServiceCode = new List<string>(); 

            while (reader.Read())
            {
                CheckServiceCode.Add(reader.GetString(0));
            }

            Console.WriteLine("No of appointments found : " + CheckServiceCode.Count);

            connection.Open();

            DateTime dd = DateTime.Now;
            string date = dd.ToString("dd/MM/yyyy");


            if (CheckServiceCode.Count == 0)
            {

                for (int i = 0; i < GetBookedServices.Count; i++)
                {
                    var selectCmd2 = connection.CreateCommand();
                    selectCmd2.CommandText = @"INSERT INTO BookedService (Email, ServiceCode, Date) VALUES ($email, $SCode, $Date)";
                    Console.WriteLine("Email : " + UserEmail);
                    Console.WriteLine("Service Code : " + GetBookedServices[i].ServiceCode);
                    Console.WriteLine("Date : " + date);
                    selectCmd2.Parameters.AddWithValue("$email", UserEmail);
                    selectCmd2.Parameters.AddWithValue("$SCode", GetBookedServices[i].ServiceCode);
                    selectCmd2.Parameters.AddWithValue("$Date", date);
                    selectCmd2.Prepare();
                    selectCmd2.ExecuteNonQuery();
                    Console.WriteLine("A record saved");
                }
            }
            else //some Services already registered. Only new Services will be registered
            {
                for (int i = 0; i < GetBookedServices.Count; i++)
                {
                    bool valid = true;
                    for (int j = 0; j < CheckServiceCode.Count; j++)
                    {

                        if (GetBookedServices[i].ServiceCode == CheckServiceCode[j])
                        {
                            valid = false;
                            Console.WriteLine("Booked appointment found!" + CheckServiceCode[j]);
                        }
                    }
                    if (valid == true)
                    {
                        var selectCmd2 = connection.CreateCommand();
                        selectCmd2.CommandText = @"INSERT INTO BookedService (Email, ServiceCode, Date) VALUES ($email, $SCode, $Date)";
                        Console.WriteLine("Email : " + UserEmail);
                        Console.WriteLine("Service Code : " + GetBookedServices[i].ServiceCode);
                        Console.WriteLine("Date : " + date);
                        selectCmd2.Parameters.AddWithValue("$email", UserEmail);
                        selectCmd2.Parameters.AddWithValue("$SCode", GetBookedServices[i].ServiceCode);
                        selectCmd2.Parameters.AddWithValue("$Date", date);
                        selectCmd2.Prepare();
                        selectCmd2.ExecuteNonQuery();
                        Console.WriteLine("A record saved");
                    }

                }
            }
            connection.Close();
            return RedirectToPage("/CustomerService/ViewBookedService", GetBookedServices);
        }
    }
}
