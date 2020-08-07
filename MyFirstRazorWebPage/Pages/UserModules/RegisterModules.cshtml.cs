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


namespace MyFirstRazorWebPage.Pages.UserModules
{

    public class RegisterModulesModel : PageModel
    {
        public string UserName;
        public const string SessionKeyName1 = "username";

        public string UserEmail;
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
                connectionStringBuilder.DataSource = "/Users/zairulmazwan/Projects/Web-App-Example/MyFirstRazorWebPage/RazorPagesMovieContext-4626ba78-c68f-4200-bc79-dd49c8d85ee3.db";
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


            return RedirectToPage("/UserModules/SuccessRegisterModule", GetRegMod);
        }
    }
}
