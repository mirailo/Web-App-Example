using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore.Storage;
using MyFirstRazorWebPage.Models;
using MyFirstRazorWebPage.Pages.DatabaseConnection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace MyFirstRazorWebPage.Pages.Users
{
    public class UserLoginModel : PageModel
    {
        private readonly RazorPagesMovieContext _context;

        public UserLoginModel(RazorPagesMovieContext context)
        {
            _context = context;
        }


        public IActionResult OnGet()
        {
            return Page();
        }

        [Required]
        [BindProperty]
        public User User { get; set; }

        [BindProperty]
        public string UserName { get; set; }

        [BindProperty]
        public string UserEmail { get; set; }


        [BindProperty]
        public string Msg { get; set; }
        

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost()
        {
            //This if statement to check the form is valid -> [Required] fields.
            if (!ModelState.IsValid)
            {
                return Page();
            }


            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            DatabaseConnect DBCon = new DatabaseConnect(); // your own class and method in DatabaseConnection folder
            string dbStringConnection = DBCon.DBStringConnection();

            connectionStringBuilder.DataSource = dbStringConnection;
            var connection = new SqliteConnection(connectionStringBuilder.ConnectionString);

            connection.Open();

            var selectCmd = connection.CreateCommand();
            selectCmd.CommandText = @"SELECT Password FROM User WHERE EmailAdd=$EmailAdd";
            selectCmd.Parameters.AddWithValue("$EmailAdd", User.EmailAdd);

            var reader = selectCmd.ExecuteReader();
            var Pwd = "";
            while (reader.Read())
            {
                Pwd = reader.GetString(0);
            }

            Console.WriteLine(Pwd);


            if (User.Password.Equals(Pwd))
                    {
                        selectCmd = connection.CreateCommand();
                        selectCmd.CommandText = @"SELECT FirstName FROM User WHERE EmailAdd=$EmailAdd";
                        selectCmd.Parameters.AddWithValue("$EmailAdd", User.EmailAdd);
                        var reader2 = selectCmd.ExecuteReader();

                        while (reader2.Read())
                        {
                            UserName = reader2.GetString(0);
                        }

                        HttpContext.Session.SetString("username", UserName);
                        HttpContext.Session.SetString("email", User.EmailAdd);
                        return RedirectToPage("/UserLoggedIn/SuccessLogin");
                    }
                    else
                    {
                        Msg = "Incorrect ID and PWD!";
                        return Page();
                    }
                
        }


        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove(UserName);
            return Page();
        }



    }

   
}
