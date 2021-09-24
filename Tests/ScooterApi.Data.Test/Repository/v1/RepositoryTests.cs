using System;
using System.Linq;
using FakeItEasy;
using FluentAssertions;
using ScooterApi.Data.Database;
using ScooterApi.Data.Repository.v1;
using ScooterApi.Data.Test.Infrastructure;
using ScooterApi.Domain.Entities;
using Xunit;

namespace ScooterApi.Data.Test.Repository.v1
{
    public class RepositoryTests : DatabaseTestBase
    {
        private readonly ScooterContext _scooterContext;
        private readonly Repository<Scooter> _testee;
        private readonly Repository<Scooter> _testeeFake;
        private readonly Scooter _newScooter;

        public RepositoryTests()
        {
            _scooterContext = A.Fake<ScooterContext>();
            _testeeFake = new Repository<Scooter>(_scooterContext);
            _testee = new Repository<Scooter>(Context);
            _newScooter = new Scooter
            {
                Id = 4,
                Time = DateTime.Now,
                ChargePercent = 100,
                CoordinateX = 55.2,
                CoordinateY = 55.2,
                InUse = false,
                ScooterId = 8
            };
        }

        [Theory]
        [InlineData(99)]
        public async void UpdateScooterAsync_WhenScooterIsNotNull_ShouldReturnScooter(byte charge)
        {
            var customer = Context.Scooter.First();
            customer.ChargePercent = charge;

            var result = await _testee.UpdateAsync(customer);

            result.Should().BeOfType<Scooter>();
            result.ChargePercent.Should().Be(charge);
        }

        [Fact]
        public void AddAsync_WhenEntityIsNull_ThrowsException()
        {
            _testee.Invoking(x => x.AddAsync(null)).Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void AddAsync_WhenExceptionOccurs_ThrowsException()
        {
            A.CallTo(() => _scooterContext.SaveChangesAsync(default)).Throws<Exception>();

            _testeeFake.Invoking(x => x.AddAsync(new Scooter())).Should().Throw<Exception>().WithMessage("entity could not be saved: Exception of type 'System.Exception' was thrown.");
        }

        [Fact]
        public async void CreateScooterAsync_WhenScooterIsNotNull_ShouldReturnScooter()
        {
            var result = await _testee.AddAsync(_newScooter);

            result.Should().BeOfType<Scooter>();
        }

        [Fact]
        public async void CreateScooterAsync_WhenScooterIsNotNull_ShouldShouldAddScooter()
        {
            var ScootersCount = Context.Scooter.Count();

            await _testee.AddAsync(_newScooter);

            Context.Scooter.Count().Should().Be(ScootersCount + 1);
        }

        [Fact]
        public void GetAll_WhenExceptionOccurs_ThrowsException()
        {
            A.CallTo(() => _scooterContext.Set<Scooter>()).Throws<Exception>();

            _testeeFake.Invoking(x => x.GetAll()).Should().Throw<Exception>().WithMessage("Couldn't retrieve entities: Exception of type 'System.Exception' was thrown.");
        }

        [Fact]
        public void UpdateAsync_WhenEntityIsNull_ThrowsException()
        {
            _testee.Invoking(x => x.UpdateAsync(null)).Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void UpdateAsync_WhenExceptionOccurs_ThrowsException()
        {
            A.CallTo(() => _scooterContext.SaveChangesAsync(default)).Throws<Exception>();

            _testeeFake.Invoking(x => x.UpdateAsync(new Scooter())).Should().Throw<Exception>().WithMessage("entity could not be updated Exception of type 'System.Exception' was thrown.");
        }
    }
}