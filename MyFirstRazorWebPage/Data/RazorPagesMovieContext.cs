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

        public DbSet<MyFirstRazorWebPage.Models.Movie> Movie { get; set; }

        public DbSet<MyFirstRazorWebPage.Models.User> User { get; set; }

        public DbSet<MyFirstRazorWebPage.Models.Promotion> Promotion { get; set; }

        public DbSet<MyFirstRazorWebPage.Models.AdminUser> AdminUser { get; set; }
    }
