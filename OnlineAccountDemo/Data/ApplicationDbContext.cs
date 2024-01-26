using OnlineAccountDemo.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace OnlineAccountDemo.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<UsersAccount> UsersAccount { get; set; }
        public DbSet<BrandCategory> BrandCategory { get; set; }
        public DbSet<BrandModel> BrandModel { get; set; }
        public DbSet<ModelColor> ModelColor { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<ModelIssues> ModelIssues { get; set; }
        public DbSet<JobStatus> JobStatus { get; set; }
        public DbSet<RepairAccessories> RepairAccessories { get; set; }
        
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

                            modelBuilder.Entity<BrandCategory>()
                    .HasMany(e => e.BrandModel)
                    .WithOne(e => e.BrandCategory)
                    .HasForeignKey(e => e.BrandId)
                    .IsRequired();

                                modelBuilder.Entity<BrandCategory>()
                    .HasMany(e => e.RepairAccessories)
                    .WithOne(e => e.BrandCategory)
                    .HasForeignKey(e => e.BrandId)
                    .IsRequired();

                                modelBuilder.Entity<ModelColor>()
                    .HasMany(e => e.RepairAccessories)
                    .WithOne(e => e.ModelColor)
                    .HasForeignKey(e => e.Colorid)
                    .IsRequired();

                                modelBuilder.Entity<Employees>()
                    .HasMany(e => e.RepairAccessories)
                    .WithOne(e => e.Employees)
                    .HasForeignKey(e => e.EmpId)
                    .IsRequired();

                                modelBuilder.Entity<JobStatus>()
                    .HasMany(e => e.RepairAccessories)
                    .WithOne(e => e.JobStatus)
                    .HasForeignKey(e => e.StatusId)
                    .IsRequired();

                                modelBuilder.Entity<ModelIssues>()
                    .HasMany(e => e.RepairAccessories)
                    .WithOne(e => e.ModelIssues)
                    .HasForeignKey(e => e.IssueId)
                    .IsRequired();

            /*                            modelBuilder.Entity<BrandModel>()
                                .HasMany(e => e.ModelColor)
                                .WithOne(e => e.BrandModel)
                                .HasForeignKey(e => e.ModelId)
                                .IsRequired();

                                            modelBuilder.Entity<BrandModel>()
                                .HasMany(e => e.ModelIssues)
                                .WithOne(e => e.BrandModel)
                                .HasForeignKey(e => e.ModelId)
                                .IsRequired();*/
        }

    }
}
