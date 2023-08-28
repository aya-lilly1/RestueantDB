using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RestueantDB.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestueantDB.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Resturant> Restueants { get; set; }
        public DbSet<Meal> Meals { get; set; }

    }
}
