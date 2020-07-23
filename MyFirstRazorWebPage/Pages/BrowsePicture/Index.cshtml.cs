using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyFirstRazorWebPage.Models;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;

namespace MyFirstRazorWebPage.Pages.BrowsePicture
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesMovieContext _context;
        private readonly IWebHostEnvironment _env;
       

        public IndexModel(RazorPagesMovieContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
          

        }

        [BindProperty]
        public IFormFile UploadFile { get; set; }

        [BindProperty]
        public string EmailAddress { get; set; }
        [BindProperty]
        public string FName { get; set; }

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            EmailAddress = id;

            //Console.WriteLine(EmailAddress);
            
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = "/Users/zairulmazwan/Projects/MyFirstRazorWebPage/MyFirstRazorWebPage/RazorPagesMovieContext-4626ba78-c68f-4200-bc79-dd49c8d85ee3.db";
            var connection = new SqliteConnection(connectionStringBuilder.ConnectionString);

            connection.Open();

            var selectCmd = connection.CreateCommand();
            selectCmd.CommandText = @"SELECT FirstName FROM User WHERE EmailAdd=$email";
            selectCmd.Parameters.AddWithValue("$email", EmailAddress);

            var reader = selectCmd.ExecuteReader();
            
           
            while (reader.Read())
            {
                FName = reader.GetString(0);
            }

            if (FName == null)
            {
                return NotFound();
            }

            return Page();
        }



        public async Task<IActionResult> OnPostAsync ()
        {

            var Fileupload = Path.Combine(_env.ContentRootPath, "ImageData", UploadFile.FileName);
           
            using(var Fstream = new FileStream(Fileupload, FileMode.Create))
            {
                await UploadFile.CopyToAsync(Fstream);
                ViewData["Message"] = "File Uploaded to Image Data folder";
               
            }


            Console.WriteLine("Email is -->"+EmailAddress);
            Console.WriteLine("File Name is -->"+UploadFile.FileName);
            Console.WriteLine("First Name is -->"+ FName);

            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = "/Users/zairulmazwan/Projects/MyFirstRazorWebPage/MyFirstRazorWebPage/RazorPagesMovieContext-4626ba78-c68f-4200-bc79-dd49c8d85ee3.db";
            var connection = new SqliteConnection(connectionStringBuilder.ConnectionString);

            connection.Open();

            var selectCmd = connection.CreateCommand();
            selectCmd.CommandText = @"INSERT INTO Picture (Email, PicName, FirstName) VALUES ($email, $PicName, $firstName)";
            selectCmd.Parameters.AddWithValue("$email", EmailAddress);
            selectCmd.Parameters.AddWithValue("$PicName", UploadFile.FileName);
            selectCmd.Parameters.AddWithValue("$firstName", FName);

            selectCmd.Prepare();
            selectCmd.ExecuteNonQuery();

            return RedirectToPage("/AdminPage/Index");

        }

      
    }
}
