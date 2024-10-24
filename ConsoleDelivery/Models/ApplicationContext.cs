using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDelivery.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Delivery> Deliveries { get; set; } = null!;
        public DbSet<Region> Regions { get; set; } = null!;

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=DeliveryDB.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Region>(model =>
            {
                model.ToTable("Regions");
                model.Property(r => r.Id).HasColumnName("RegionId");
                model.Property(r => r.RegionName).HasColumnName("RegionName");
            });

            modelBuilder.Entity<Delivery>(model => 
            {
                model.ToTable("Deliveries")
                    .HasOne(d => d.Region)
                    .WithMany(r => r.Deliveries)
                    .HasForeignKey(d => d.RegionId);
                model.HasKey(r => r.Id);
            });
        }
    }
}
