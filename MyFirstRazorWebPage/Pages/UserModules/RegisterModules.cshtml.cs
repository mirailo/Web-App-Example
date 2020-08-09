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

namespace MyFirstRazorWebPage.Pages.UserModules
{

    public class RegisterModulesModel : PageModel
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
        public List<Modules> GetRegMod { get; set; } 

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
                Console.WriteLine("Retrieving modules");

                var connectionStringBuilder = new SqliteConnectionStringBuilder();
                DatabaseConnect DBCon = new DatabaseConnect(); // your own class and method in DatabaseConnection folder
                string dbStringConnection = DBCon.DBStringConnection();

                connectionStringBuilder.DataSource = dbStringConnection;
                var connection = new SqliteConnection(connectionStringBuilder.ConnectionString);

                connection.Open();

                var selectCmd = connection.CreateCommand();

                selectCmd.CommandText = @"SELECT * FROM Modules ORDER BY ModLevel";
                var reader = selectCmd.ExecuteReader();


               
                while (reader.Read())
                {
                    //MCode = reader.GetString(1);
                    //MName = reader.GetString(2);
                    //MLevel = reader.GetString(3);
                    //MSemester = reader.GetString(4);

                    Modules mod = new Modules();
                    mod.ModCode = reader.GetString(1);
                    mod.ModName = reader.GetString(2);
                    mod.ModLevel = reader.GetInt32(3);
                    mod.ModSemester = reader.GetInt32(4);

                
                    ModRecords.Add(mod);
                    IsSelect.Add(false);
                   

                    //Console.WriteLine(mod.ModCode);
                    //Console.WriteLine(mod.ModName);
                    //Console.WriteLine(mod.ModLevel);
                    //Console.WriteLine(mod.ModSemester);

                    /*
                    Console.WriteLine(reader.GetString(1));
                    Console.WriteLine(reader.GetString(2));
                    Console.WriteLine(reader.GetInt32(3));
                    Console.WriteLine(reader.GetInt32(4));*/
                }

                return Page();
            }

        }

        public async Task<IActionResult> OnPostAsync()
        {
            Console.WriteLine("Registering Module");
            Console.WriteLine("IsSelect : "+IsSelect[3]);
           


            for (int i=0; i<ModRecords.Count(); i++)
            {
                if (IsSelect[i] == true)
                {
                    Console.WriteLine(ModRecords[i].ModName);
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
            selectCmd.CommandText = @"SELECT ModCode FROM RegisteredModule WHERE StudenEmail=$email";
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
                    selectCmd2.CommandText = @"INSERT INTO RegisteredModule (StudenEmail, ModCode, Date) VALUES ($email, $MCode, $Date)";
                    Console.WriteLine("Email : " + UserEmail);
                    Console.WriteLine("Mod Code : " + GetRegMod[i].ModCode);
                    Console.WriteLine("Date : " + date);
                    selectCmd2.Parameters.AddWithValue("$email", UserEmail);
                    selectCmd2.Parameters.AddWithValue("$MCode", GetRegMod[i].ModCode);
                    selectCmd2.Parameters.AddWithValue("$Date", date);
                    selectCmd2.Prepare();
                    selectCmd2.ExecuteNonQuery();
                    Console.WriteLine("A record saved");
                }
            }
            else
            {
                for (int i = 0; i < GetRegMod.Count; i++)
                {
                    bool valid = true;
                    for (int j = 0; j < CheckModuleCode.Count; j++)
                    {
                        
                        if (GetRegMod[i].ModCode == CheckModuleCode[j])
                        {
                            valid = false;
                            Console.WriteLine("Registered module found!" + CheckModuleCode[j]);
                        }
                    }
                    if (valid == true)
                    {
                        var selectCmd2 = connection.CreateCommand();
                        selectCmd2.CommandText = @"INSERT INTO RegisteredModule (StudenEmail, ModCode, Date) VALUES ($email, $MCode, $Date)";
                        Console.WriteLine("Email : " + UserEmail);
                        Console.WriteLine("Mod Code : " + GetRegMod[i].ModCode);
                        Console.WriteLine("Date : " + date);
                        selectCmd2.Parameters.AddWithValue("$email", UserEmail);
                        selectCmd2.Parameters.AddWithValue("$MCode", GetRegMod[i].ModCode);
                        selectCmd2.Parameters.AddWithValue("$Date", date);
                        selectCmd2.Prepare();
                        selectCmd2.ExecuteNonQuery();
                        Console.WriteLine("A record saved");
                    }

                }
            }

            return RedirectToPage("/UserModules/ViewRegisteredModule", GetRegMod);
        }
    }
}
