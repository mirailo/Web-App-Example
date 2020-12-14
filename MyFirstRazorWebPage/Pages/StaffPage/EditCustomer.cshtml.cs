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
    public class EditCustomerModel : PageModel
    {
        private readonly HairSalonApptContext _context;
        private readonly IWebHostEnvironment _env;

        public EditCustomerModel(HairSalonApptContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [BindProperty]
        public Customer Customer { get; set; }

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

            Customer = await _context.Customer.FirstOrDefaultAsync(m => m.ID == id);

            if (Customer == null)
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
            selectCmd.Parameters.AddWithValue("$email", Customer.EmailAdd);

            var reader = selectCmd.ExecuteReader();
            var fileName = "";

            while (reader.Read())
            {
                fileName = reader.GetString(0);
            }

            if (string.IsNullOrEmpty(fileName))
            {
                pathPicture = "DefaultPic.jpeg";
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

            _context.Attach(Customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(Customer.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./CustomerDetails");
        }

        private bool UserExists(int id)
        {

            return _context.Customer.Any(e => e.ID == id);
        }

    }

}
