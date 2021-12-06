using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Time.Models;

namespace Time.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employee { get; set; }
        public DbSet<Owner> Owner { get; set; }
        public DbSet<PlaceOfWork> PlaceOfWork { get; set; }
        public DbSet<Position> Position { get; set; }
        public DbSet<TimeWorked> TimeWorked { get; set; }
    }
}
