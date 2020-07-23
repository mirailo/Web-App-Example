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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace MyFirstRazorWebPage.Pages.AdminPage
{
    public class Index2Model : PageModel
    {

        private readonly RazorPagesMovieContext _context;

        public Index2Model(RazorPagesMovieContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }


        //[Required]
        //[BindProperty]
        //public User User { get; set; }

        [Required]
        [BindProperty]
        public AdminUser AdminUser { get; set; }


        [BindProperty]
        public string UserName { get; set; }

        public string Msg { get; set; }


        
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
                connectionStringBuilder.DataSource = "/Users/zairulmazwan/Projects/MyFirstRazorWebPage/MyFirstRazorWebPage/RazorPagesMovieContext-4626ba78-c68f-4200-bc79-dd49c8d85ee3.db";
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

                    HttpContext.Session.SetString("username", JsonSerializer.Serialize(UserName));
                   
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