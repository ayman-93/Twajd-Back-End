using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Seed Roles
            Role Owner = new Role() { Id = Guid.Parse("7f5dc82f-22c7-4eb1-bba9-5c442f611f8c"), Name = "Owner", NormalizedName = "OWNER" };
            Role Manager = new Role() { Id = Guid.Parse("b59feb1b-4c4f-4b0e-99d8-349f2310b850"), Name = "Manager", NormalizedName = "MANAGER" };
            Role Employee = new Role() { Id = Guid.Parse("670e0b21-8f65-42a1-8bd1-f171b5580408"), Name = "Employee", NormalizedName = "EMPLOYEE" };

            // Seed Owner
            ApplicationUser ownerUser = new ApplicationUser()
            {
                Id = Guid.Parse("95356489-d70a-4f8f-9b9b-4e4f53e0b1b1"),
                UserName = "admin@twajd.com",
                NormalizedUserName = "ADMIN@TWAJD.COM",
                Email = "admin@twajd.com",
                NormalizedEmail = "ADMIN@TWAJD.COM",
                PasswordHash = "AQAAAAEAACcQAAAAENQxzBGBKCDd0AqOx4vBFw60lso4uaVm9wKAtzEXeZ7KxNBFPArT0jzEMbhWc32iWQ==",
                SecurityStamp = "3S3U6NV6X44VGG2UIHFK4WPJOJSXAFIJ",
                ConcurrencyStamp = "4d0bf24b-c311-4720-811f-152517f252a6"
            };

            IdentityUserRole<Guid> aspNetUserRoles = new IdentityUserRole<Guid>() { UserId = Guid.Parse("95356489-d70a-4f8f-9b9b-4e4f53e0b1b1"), RoleId = Guid.Parse("7f5dc82f-22c7-4eb1-bba9-5c442f611f8c") };
            modelBuilder.Entity<Role>().HasData(Owner, Manager, Employee);
            modelBuilder.Entity<ApplicationUser>().HasData(ownerUser);
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(aspNetUserRoles);

        }
    }
}
