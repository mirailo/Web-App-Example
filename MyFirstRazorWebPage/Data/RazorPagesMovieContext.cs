using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyFirstRazorWebPage.Models;

    public class RazorPagesMovieContext : DbContext
    {
        public RazorPagesMovieContext (DbContextOptions<RazorPagesMovieContext> options)
            : base(options)
        {
        }

        

        public DbSet<MyFirstRazorWebPage.Models.User> User { get; set; }

      

        public DbSet<MyFirstRazorWebPage.Models.AdminUser> AdminUser { get; set; }

        public DbSet<MyFirstRazorWebPage.Models.UserAccessData> UserAccessData { get; set; }

        public DbSet<MyFirstRazorWebPage.Models.Module> Module { get; set; }

    }
