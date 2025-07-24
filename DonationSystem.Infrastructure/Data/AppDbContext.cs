using DonationSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationSystem.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) 
        { 
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Donation> Donations => Set<Donation>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.FullName).IsRequired();
                entity.Property(u => u.Email).IsRequired();
            });

            modelBuilder.Entity<Donation>(entity =>
            {
                entity.HasKey(d => d.Id);
                entity.Property(d => d.Amount).IsRequired();

                entity.HasOne(d => d.User)
                      .WithMany(u => u.Donations)
                      .HasForeignKey(d => d.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(d => d.Images)
                      .WithOne(i => i.Donation)
                      .HasForeignKey(i => i.DonationId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleName });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);
        }
    }
}