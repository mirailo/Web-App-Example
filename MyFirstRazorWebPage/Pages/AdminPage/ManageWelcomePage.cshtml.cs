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

namespace MyFirstRazorWebPage.Pages.AdminPage
{
    
    public class ManageWelcomePageModel : PageModel
    {
        private readonly RazorPagesMovieContext _context;
        private readonly IWebHostEnvironment _env;

        public ManageWelcomePageModel(RazorPagesMovieContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [BindProperty]
        public WelcomPage WPage { get; set; }

        public string dateUpdate; //Global variable work perfectly

        [BindProperty]
        public string CurMsg { get; set; } //Global variable work perfectly

   
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
                    // Read the stream to a string, and write the string to the console.
                    String line = sr.ReadToEnd();
                    //Console.WriteLine(line);
                    CurMsg = line;
                    Console.WriteLine("The current text before update is : "+CurMsg);
                    //but when i want to assign the value to the model
                    //WPage.Message = CurMsg; //this does not work.


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
            // Set a variable to the Documents path.
            string WelcomeFile = "Welcome.txt";
            var FileLocation = Path.Combine(_env.WebRootPath, "WelcomeMessage", WelcomeFile);


            // Write the specified text asynchronously to a new file named "WriteTextAsync.txt".
            using (StreamWriter outputFile = new StreamWriter(FileLocation))
            {
                await outputFile.WriteAsync(CurMsg);
            }

           
            return RedirectToPage("/AdminPage/Index");
        }




        }
}
