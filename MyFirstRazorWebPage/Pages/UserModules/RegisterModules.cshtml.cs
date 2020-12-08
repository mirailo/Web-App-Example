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

namespace MyFirstRazorWebPage.Pages.UserServices
{

    public class RegisterServicesModel : PageModel
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
        public List<Services> GetRegMod { get; set; } 

        public async Task<IActionResult> OnGetAsync()
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

                selectCmd.CommandText = @"SELECT * FROM Services ORDER BY ServiceDuration";
                var reader = selectCmd.ExecuteReader();

                while (reader.Read())
                {
                   
                    Services ser = new Services();
                    ser.ServiceCode = reader.GetString(1);
                    ser.ServiceName = reader.GetString(2);
                    ser.ServiceDuration = reader.GetInt32(3);
                    ser.ServicePrice = reader.GetInt32(4);

                    ModRecords.Add(ser);
                    IsSelect.Add(false);
                   
                }
                connection.Close();
                return Page();
            }

        }

        public async Task<IActionResult> OnPostAsync()
        {
            Console.WriteLine("Registering Module");
         
            for (int i=0; i<ModRecords.Count(); i++)
            {
                if (IsSelect[i] == true)
                {
                    Console.WriteLine(ModRecords[i].ServiceName);
                    GetRegMod.Add(ModRecords[i]);
                }
            }

            Console.WriteLine("Registered Module/s : " + GetRegMod.Count());

            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            DatabaseConnect DBCon = new DatabaseConnect(); // your own class and method in DatabaseConnection folder
            string dbStringConnection = DBCon.DBStringConnection();

            connectionStringBuilder.DataSource = dbStringConnection;
            var connection = new SqliteConnection(connectionStringBuilder.ConnectionString);

            connection.Open();

            var selectCmd = connection.CreateCommand();
            selectCmd.CommandText = @"SELECT ServiceCode FROM RegisteredModule WHERE StudenEmail=$email";
            Console.WriteLine("Email : "+UserEmail);
            selectCmd.Parameters.AddWithValue("$email", UserEmail);
            var reader = selectCmd.ExecuteReader();

            List<string> CheckModuleCode = new List<string>(); //a variable use be assigned for the CodeModule registered found

            while (reader.Read())
            {
                CheckModuleCode.Add(reader.GetString(0));
            }

            Console.WriteLine("No of module found : "+ CheckModuleCode.Count);
          
            connection.Open();
           
            DateTime dd = DateTime.Now;
            string date = dd.ToString("dd/MM/yyyy");

            
            if (CheckModuleCode.Count==0)
            {
                
                for (int i=0; i< GetRegMod.Count; i++)
                {
                    var selectCmd2 = connection.CreateCommand();
                    selectCmd2.CommandText = @"INSERT INTO RegisteredModule (StudenEmail, ServiceCode, Date) VALUES ($email, $MCode, $Date)";
                    Console.WriteLine("Email : " + UserEmail);
                    Console.WriteLine("Service Code : " + GetRegMod[i].ServiceCode);
                    Console.WriteLine("Date : " + date);
                    selectCmd2.Parameters.AddWithValue("$email", UserEmail);
                    selectCmd2.Parameters.AddWithValue("$MCode", GetRegMod[i].ServiceCode);
                    selectCmd2.Parameters.AddWithValue("$Date", date);
                    selectCmd2.Prepare();
                    selectCmd2.ExecuteNonQuery();
                    Console.WriteLine("A record saved");
                }
            }
            else //some Services already registered. Only new Services will be registered
            {
                for (int i = 0; i < GetRegMod.Count; i++)
                {
                    bool valid = true;
                    for (int j = 0; j < CheckModuleCode.Count; j++)
                    {
                        
                        if (GetRegMod[i].ServiceCode == CheckModuleCode[j])
                        {
                            valid = false;
                            Console.WriteLine("Registered module found!" + CheckModuleCode[j]);
                        }
                    }
                    if (valid == true)
                    {
                        var selectCmd2 = connection.CreateCommand();
                        selectCmd2.CommandText = @"INSERT INTO RegisteredModule (StudenEmail, ServiceCode, Date) VALUES ($email, $MCode, $Date)";
                        Console.WriteLine("Email : " + UserEmail);
                        Console.WriteLine("Service Code : " + GetRegMod[i].ServiceCode);
                        Console.WriteLine("Date : " + date);
                        selectCmd2.Parameters.AddWithValue("$email", UserEmail);
                        selectCmd2.Parameters.AddWithValue("$MCode", GetRegMod[i].ServiceCode);
                        selectCmd2.Parameters.AddWithValue("$Date", date);
                        selectCmd2.Prepare();
                        selectCmd2.ExecuteNonQuery();
                        Console.WriteLine("A record saved");
                    }
                    
                }
            }
            connection.Close();
            return RedirectToPage("/UserServices/ViewRegisteredModule", GetRegMod);
        }
    }
}
