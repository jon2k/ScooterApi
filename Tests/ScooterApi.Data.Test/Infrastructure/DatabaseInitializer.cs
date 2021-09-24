using System;
using System.Linq;
using ScooterApi.Data.Database;
using ScooterApi.Domain.Entities;

namespace ScooterApi.Data.Test.Infrastructure
{
    public class DatabaseInitializer
    {
        public static void Initialize(ScooterContext context)
        {
            if (context.Scooter.Any())
            {
                return;
            }

            Seed(context);
        }

        private static void Seed(ScooterContext context)
        {
            var customers = new[]
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

            context.Scooter.AddRange(customers);
            context.SaveChanges();
        }
    }
}