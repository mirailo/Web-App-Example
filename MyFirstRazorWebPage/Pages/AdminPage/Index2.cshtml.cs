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

namespace MyFirstRazorWebPage.Pages.AdminPage
{
    public class Index2Model : PageModel
    {

        private readonly RazorPagesMovieContext _context;

        public string UserName;
        public const string SessionKeyName1 = "username";

        public string SessionID;
        public const string SessionKeyID = "sessionID";

        public string UserEmail;
        public const string SessionKeyName2 = "email";

        [Required]
        [BindProperty]
        public AdminUser AdminUser { get; set; }

        public string Msg { get; set; }

      
        public Index2Model(RazorPagesMovieContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            /*UserName = HttpContext.Session.GetString(SessionKeyName1);
            UserEmail = HttpContext.Session.GetString(SessionKeyName2);
            Console.WriteLine("Current session: " + UserName);

            if (string.IsNullOrEmpty(UserName))
            {
                Console.WriteLine("Session ended");
                return Page();


            }
            else
            {
                return RedirectToPage("/AdminPage/Index");
                
            }*/

            return Page();

        }

        public IActionResult OnPost()
        {
            /*
            //This if statement to check the form is valid -> [Required] fields.
            if (!ModelState.IsValid)
            {
                return Page();
            }
            */

           
        
            if (string.IsNullOrEmpty(AdminUser.StaffNo) || string.IsNullOrEmpty(AdminUser.AdminPassword))
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
                selectCmd.CommandText = @"SELECT AdminPassword FROM AdminUser WHERE StaffNo=$StaffNo";
                selectCmd.Parameters.AddWithValue("$StaffNo", AdminUser.StaffNo);

                var reader = selectCmd.ExecuteReader();
                var Pwd = "";
                while (reader.Read())
                {
                    Pwd = reader.GetString(0);
                }

                Console.WriteLine(Pwd);

                if (AdminUser.AdminPassword.Equals(Pwd))
                {
                    selectCmd = connection.CreateCommand();
                    selectCmd.CommandText = @"SELECT FirstName FROM AdminUser WHERE StaffNo=$StaffNo";
                    selectCmd.Parameters.AddWithValue("$StaffNo", AdminUser.StaffNo);
                    var reader2 = selectCmd.ExecuteReader();

                    while (reader2.Read())
                    {
                        UserName = reader2.GetString(0);
                    }

                    //HttpContext.Session.SetString("username", JsonSerializer.Serialize(UserName));
                   
                    HttpContext.Session.SetString("username", UserName);
                    SessionID = HttpContext.Session.Id;
                    HttpContext.Session.SetString("sessionID", SessionID);

                    //HttpContext.Response.Cookies.Append("username", UserName);




                    Console.WriteLine("Session ID : "+ SessionID);
                    return RedirectToPage("/AdminPage/Index");
                }
                else
                {
                    Msg = "Incorrect ID and PWD!";
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