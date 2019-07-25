using System;
using System.Collections.Generic;
using System.Text;
using BackendCapstone.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BackendCapstone.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options){ }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
