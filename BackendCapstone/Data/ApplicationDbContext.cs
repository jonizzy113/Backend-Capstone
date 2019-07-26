using System;
using System.Collections.Generic;
using System.Text;
using BackendCapstone.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BackendCapstone.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) { }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<UserType> UserType { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ApplicationUser user = new ApplicationUser
            {
                FirstName = "Jonathan",
                LastName = "Schaffer",
                Address = "123 Infinity Way",
                UserName = "jon@jon.com",
                NormalizedUserName = "JON@JON.COM",
                Email = "jon@jon.com",
                NormalizedEmail = "JON@JON.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = "7f434309-a4d9-48e9-9ebb-8803db794577",
                Id = "00000000-ffff-ffff-ffff-ffffffffffff",
                UserTypeId = 1
            };
            var passwordHash = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHash.HashPassword(user, "Admin8*");
            modelBuilder.Entity<ApplicationUser>().HasData(user);


            modelBuilder.Entity<UserType>().HasData(
            new UserType()
            {
                Id = 1,
                Type = "Office"
            },
            new UserType()
            {
                Id = 2,
                Type = "Broker"
            },
            new UserType()
            {
                Id = 3,
                Type = "Salesman"
            }
            );
            modelBuilder.Entity<Customer>().HasData(
                new Customer()
                {
                    CustomerId = 1,
                    FirstName = "Jameka",
                    LastName = "Echols",
                    Address = "500 Interstate Blvd S",
                    Email = "jameka@jameka.com"
                },
                new Customer()
                {
                    CustomerId = 2,
                    FirstName = "Billy",
                    LastName = "Mathison",
                    Address = "522 Buffalo Creek Road",
                    Email = "billy@billy.com"
                }
             );
        }
    }
}
