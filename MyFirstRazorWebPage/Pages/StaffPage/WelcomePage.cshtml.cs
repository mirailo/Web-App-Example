using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using MyFirstRazorWebPage.Models;

namespace MyFirstRazorWebPage.Pages.StaffPage
{

    public class WelcomePageModel : PageModel
    {
        private readonly HairSalonApptContext _context;
        private readonly IWebHostEnvironment _env;

        public WelcomePageModel(HairSalonApptContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [BindProperty]
        public WelcomePage WPage { get; set; }

        public string dateUpdate; //Global variable work perfectly

        [BindProperty]
        public string Msg { get; set; } //Global variable work perfectly


        public IActionResult OnGet()
        {
            DateTime dd = DateTime.Now;
            string date = dd.ToString("dd/MM/yyyy");
            Console.WriteLine(date);
            dateUpdate = date;


            string WelcomeFile = "Welcome.txt";
            var FileLocation = Path.Combine(_env.WebRootPath, "WelcomeMessage", WelcomeFile);

            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(FileLocation))
                {
                    
                    String line = sr.ReadToEnd();
                    Msg = line;
                    Console.WriteLine("The current text before update is : " + Msg);


                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            string WelcomeFile = "Welcome.txt";
            var FileLocation = Path.Combine(_env.WebRootPath, "WelcomeMessage", WelcomeFile);

            using (StreamWriter outputFile = new StreamWriter(FileLocation))
            {
                await outputFile.WriteAsync(Msg);
            }


            return RedirectToPage("/StaffPage/Index");
        }




    }
}