using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using Twajd_Back_End.Core.Models;
using Twajd_Back_End.Core.Models.Auth;

namespace Twajd_Back_End.DataAccess.Repositories
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser, Role, Guid>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        //public DbSet<FaskeUser> FaskeUser { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Attendance> Attendance { get; set; }
    }
}
