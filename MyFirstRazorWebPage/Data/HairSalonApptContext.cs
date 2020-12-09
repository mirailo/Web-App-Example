using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyFirstRazorWebPage.Models;

    public class HairSalonApptContext : DbContext
    {

    public HairSalonApptContext (DbContextOptions<HairSalonApptContext> options)
            : base(options)
        {
        }

        

        public DbSet<MyFirstRazorWebPage.Models.User> User { get; set; }

        public DbSet<MyFirstRazorWebPage.Models.StaffUser> StaffUser { get; set; }

        public DbSet<MyFirstRazorWebPage.Models.Services> Services { get; set; }

        //public DbSet<MyFirstRazorWebPage.Models.UserAccessData> UserAccessData { get; set; }

        

    }
