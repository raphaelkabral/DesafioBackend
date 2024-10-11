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

            modelBuilder.Entity<Lease>()
           .HasOne(r => r.PlanLease)
           .WithMany()
           .HasForeignKey(r => r.PlanLeaseId); // Defina a chave estrangeira

            modelBuilder.Entity<PlanLease>()
                .HasData(
                    new PlanLease { Id = 1, Name = "7 Dias", DailyRate = 30, DurationDays = 7 },
                    new PlanLease { Id = 2, Name = "15 Dias", DailyRate = 28, DurationDays = 15 },
                    new PlanLease { Id = 3, Name = "30 Dias", DailyRate = 22, DurationDays = 30 },
                    new PlanLease { Id = 4, Name = "45 Dias", DailyRate = 20, DurationDays = 45 },
                    new PlanLease { Id = 5, Name = "50 Dias", DailyRate = 18, DurationDays = 50 }
                );
        }
    }
}