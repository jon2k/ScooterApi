using System;
using Microsoft.EntityFrameworkCore;
using ScooterApi.Domain.Entities;

namespace ScooterApi.Data.Database
{
    public class ScooterContext : DbContext
    {
        public ScooterContext()
        {
        }

        public ScooterContext(DbContextOptions<ScooterContext> options)
            : base(options)
        {
           /* Database.EnsureDeleted();
            Database.EnsureCreated();
            var scooters = new[]
            {
                new Scooter
                {
                    Id = 1,
                    Time = DateTime.Now,
                    ChargePercent = 100,
                    CoordinateX = 55,
                    CoordinateY = 55,
                    InUse = false,
                    ScooterId = 5
                },
                new Scooter
                {
                    Id = 2,
                    Time = DateTime.Now,
                    ChargePercent = 100,
                    CoordinateX = 55.1,
                    CoordinateY = 55.1,
                    InUse = false,
                    ScooterId = 6
                },
                new Scooter
                {
                    Id = 3,
                    Time = DateTime.Now,
                    ChargePercent = 100,
                    CoordinateX = 55.2,
                    CoordinateY = 55.2,
                    InUse = false,
                    ScooterId = 7
                },
            };
            
            Scooter.AddRange(scooters);
            SaveChanges();*/
        }

        public DbSet<Scooter> Scooter { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Scooter>(entity =>
            {
                entity.Property(e => e.ScooterId)
                    .IsRequired()
                    .ValueGeneratedNever();
                entity.Property(e => e.Time)
                    .IsRequired();
                entity.Property(e => e.ChargePercent)
                    .IsRequired();
                entity.Property(e => e.CoordinateX)
                    .IsRequired();
                entity.Property(e => e.CoordinateY)
                    .IsRequired();
                entity.Property(e => e.InUse)
                    .IsRequired();
            });
        }
    }
}
