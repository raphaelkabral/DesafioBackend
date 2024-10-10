using LeaseControl.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace LeaseControl.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Motorcycle> Motorcycles { get; set; }
        public DbSet<Lease> Leasess { get; set; }
        public DbSet<DeliveryMan> Deliverymens { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Motorcycle>()
           .HasIndex(m => m.Plate)
           .IsUnique();

            modelBuilder.Entity<DeliveryMan>()
                .HasIndex(e => e.CNPJ)
                .IsUnique();

            modelBuilder.Entity<DeliveryMan>()
                .HasIndex(e => e.CNH)
                .IsUnique();
        }
    }
}