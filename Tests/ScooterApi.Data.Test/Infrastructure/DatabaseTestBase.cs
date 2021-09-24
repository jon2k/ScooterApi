using System;
using Microsoft.EntityFrameworkCore;
using ScooterApi.Data.Database;

namespace ScooterApi.Data.Test.Infrastructure
{
    public class DatabaseTestBase : IDisposable
    {
        protected readonly ScooterContext Context;

        public DatabaseTestBase()
        {
            var options = new DbContextOptionsBuilder<ScooterContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            Context = new ScooterContext(options);

            Context.Database.EnsureCreated();

            DatabaseInitializer.Initialize(Context);
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();

            Context.Dispose();
        }
    }
}