using OnlineAccountDemo.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace OnlineAccountDemo.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<UsersAccount> UsersAccount { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

               modelBuilder.Entity<Users>()
               .HasOne(e => e.UsersAccount)
               .WithOne(e => e.Users)
               .HasForeignKey<UsersAccount>(e => e.UserId)
               .IsRequired();
        }

    }
}
