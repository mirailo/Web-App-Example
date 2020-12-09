using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore.Storage;
using MyFirstRazorWebPage.Models;
using MyFirstRazorWebPage.Pages.DatabaseConnection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace MyFirstRazorWebPage.Pages.StaffPage
{
    public class Index2Model : PageModel
    {

        private readonly HairSalonApptContext _context;

        public string UserName;
        public const string SessionKeyName1 = "username";

        public string SessionID;
        public const string SessionKeyID = "sessionID";

        public string UserEmail;
        public const string SessionKeyName2 = "email";

        [Required]
        [BindProperty]
        public StaffUser StaffUser { get; set; }

        public string Msg { get; set; }

      
        public Index2Model(HairSalonApptContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            Console.WriteLine("onget");
            return Page();

        }

        public IActionResult OnPost()
        {
            

           
        
            if (string.IsNullOrEmpty(StaffUser.StaffNo) || string.IsNullOrEmpty(StaffUser.StaffPassword))
            {
                Msg = "Please input Staff No and Password";
                return Page();
            }
            else
            {
                var connectionStringBuilder = new SqliteConnectionStringBuilder();
                DatabaseConnect DBCon = new DatabaseConnect();
                string dbStringConnection = DBCon.DBStringConnection();
                connectionStringBuilder.DataSource = dbStringConnection;
                var connection = new SqliteConnection(connectionStringBuilder.ConnectionString);

                connection.Open();

                var selectCmd = connection.CreateCommand();
                selectCmd.CommandText = @"SELECT StaffPassword FROM StaffUser WHERE StaffNo=$StaffNo";
                selectCmd.Parameters.AddWithValue("$StaffNo", StaffUser.StaffNo);

                var reader = selectCmd.ExecuteReader();
                var Pwd = "";
                while (reader.Read())
                {
                    Pwd = reader.GetString(0);
                }

                Console.WriteLine(Pwd);

                if (StaffUser.StaffPassword.Equals(Pwd))
                {
                    selectCmd = connection.CreateCommand();
                    selectCmd.CommandText = @"SELECT FirstName FROM StaffUser WHERE StaffNo=$StaffNo";
                    selectCmd.Parameters.AddWithValue("$StaffNo", StaffUser.StaffNo);
                    var reader2 = selectCmd.ExecuteReader();

                    while (reader2.Read())
                    {
                        UserName = reader2.GetString(0);
                    }
                   
                    HttpContext.Session.SetString("username", UserName);
                    SessionID = HttpContext.Session.Id;
                    HttpContext.Session.SetString("sessionID", SessionID);





                    Console.WriteLine("Session ID : "+ SessionID);
                    return RedirectToPage("/StaffPage/Index");
                }
                else
                {
                    Msg = "Incorrect login ID and/or Password!";
                    return Page();
                }
            }


        }


        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove(UserName);
            return Page();
        }

        

    }


}