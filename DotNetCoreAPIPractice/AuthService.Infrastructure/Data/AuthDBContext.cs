using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AuthService.Domain;
using AuthService.Domain.Entities;

namespace AuthService.Infrastructure.Data
{
    public class AuthDBContext : DbContext
    {
        public AuthDBContext(DbContextOptions<AuthDBContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Team> Teams { get; set; }


    }
}
