using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MyFirstRazorWebPage.Models;
using System.IO;
using MyFirstRazorWebPage.Pages.DatabaseConnection;


namespace MyFirstRazorWebPage.Pages.StaffPage
{
    public class EditUserModel : PageModel
    {
        private readonly HairSalonApptContext _context;
        private readonly IWebHostEnvironment _env;

        public EditUserModel(HairSalonApptContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [BindProperty]
        public User User { get; set; }

        [BindProperty]
        public Picture UserPic { get; set; }

        [BindProperty]
        public IFormFile GetFile { get; set; }

        [BindProperty]
        public string pathPicture { get; set; }

        [BindProperty]
        public string FileName { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User = await _context.User.FirstOrDefaultAsync(m => m.ID == id);

            if (User == null)
            {
                return NotFound();
            }

            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            DatabaseConnect DBCon = new DatabaseConnect();
            string dbStringConnection = DBCon.DBStringConnection();

            connectionStringBuilder.DataSource = dbStringConnection;
            var connection = new SqliteConnection(connectionStringBuilder.ConnectionString);

            connection.Open();

            var selectCmd = connection.CreateCommand();
            selectCmd.CommandText = @"SELECT PicName FROM Picture WHERE Email=$email";
            selectCmd.Parameters.AddWithValue("$email", User.EmailAdd);

            var reader = selectCmd.ExecuteReader();
            var fileName = "";

            while (reader.Read())
            {
                fileName = reader.GetString(0);
            }

            if (string.IsNullOrEmpty(fileName))
            {
                pathPicture = "DefaulPic.jpeg";
                Console.WriteLine("Default pic : " + pathPicture);
                return Page();
            }

            pathPicture = fileName;

            Console.WriteLine("File name is : " + fileName);
            pathPicture = fileName;



            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(User).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(User.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./UserDetails");
        }

        private bool UserExists(int id)
        {

            return _context.User.Any(e => e.ID == id);
        }

    }

}
