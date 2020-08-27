using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MyFirstRazorWebPage.Models;

namespace MyFirstRazorWebPage.Pages.AdminPage
{
    public class ManagePicture
    {
        private readonly IWebHostEnvironment _env;

        public ManagePicture(IWebHostEnvironment env)
        {
            
            _env = env;
        }
        string picName = "";


        public void DeletePic(string picName)
        {
         
            this.picName = picName;
            Console.WriteLine(picName);
            if (picName != null)
            {

                string RetrieveImage = Path.Combine(_env.WebRootPath, "Images", picName);
                System.IO.File.Delete(RetrieveImage);

            }

        }
    }
}
