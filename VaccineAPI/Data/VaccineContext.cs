using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace VaccineAPI.Models
{
    public class VaccineContext : IdentityDbContext<User>
    {
        public VaccineContext(DbContextOptions<VaccineContext> options)
            : base(options)
        {
        }

        public DbSet<Address> Address { get; set; }
        public DbSet<Vaccine> Vaccines { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<VaccineOrder> VaccineOrders { get; set; }
        public DbSet<Warehouse> Warehouse { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Vaccine>().HasData(
                new Vaccine
                {
                    VaccineId = Guid.NewGuid(),
                    VaccineName = "Pfizer - BioNTech",
                    Manufacturer = "BioNTech, Fosun Pharma, Pfizer",
                    StorageTemperature = -70,
                    Doses = 2,
                    Type = "RNA",
                    AdministrationMethod = "Intramuscular injection"
                },
                new Vaccine
                {
                    VaccineId = Guid.NewGuid(),
                    VaccineName = "AstraZeneca",
                    Manufacturer = "AstraZeneca, University of Oxford",
                    StorageTemperature = -40,
                    Doses = 2,
                    Type = "RNA",
                    AdministrationMethod = "Intramuscular injection"
                },
                new Vaccine
                {
                    VaccineId = Guid.NewGuid(),
                    VaccineName = "Moderna",
                    Manufacturer = "Moderna, NIAID",
                    StorageTemperature = -20,
                    Doses = 2,
                    Type = "RNA",
                    AdministrationMethod = "Intramuscular injection"
                });

            modelBuilder.Entity<VaccineOrder>()
                .HasKey(k => new { k.VaccineId, k.OrderId });

            //modelBuilder.Entity<VaccineOrder>()
            //    .HasOne(v => v.Vaccine)
            //    .WithMany(v => v.VaccineOrders)
            //    .HasForeignKey(v => v.VaccineId);

            modelBuilder.Entity<VaccineOrder>()
                .HasOne(o => o.Order)
                .WithMany(o => o.VaccineOrders)
                .HasForeignKey(o => o.OrderId);
        }
    }
}
