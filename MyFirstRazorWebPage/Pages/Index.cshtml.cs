using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MyFirstRazorWebPage.Models;

namespace MyFirstRazorWebPage.Pages
{
    public class IndexModel : PageModel
    {

        private readonly ILogger<IndexModel> _logger;
        private readonly RazorPagesMovieContext _context;
        private readonly IWebHostEnvironment _env;

        public string Message { get; set; }

        public IndexModel(ILogger<IndexModel> logger, RazorPagesMovieContext context, IWebHostEnvironment env)
        {
            _logger = logger;
            _context = context;
            _env = env;
        }

        public IActionResult OnGet()
        {
            string WelcomeFile = "Welcome.txt";
            var FileLocation = Path.Combine(_env.WebRootPath, "WelcomeMessage", WelcomeFile);
            Console.WriteLine("Message file location : "+FileLocation);

            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(FileLocation))
                {
                    // Read the stream to a string, and write the string to the console.
                    String line = sr.ReadToEnd();
                    Console.WriteLine(line);
                    Message = line;
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }


            return Page();

        }
    }
}
