using System.Collections.Generic;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using ScooterApi.Data.Repository.v1;
using ScooterApi.Domain.Entities;
using ScooterApi.Service.v1.Query;
using Xunit;

namespace ScooterApi.Service.Test.v1.Query
{
    public class GetScooterByIdQueryHandlerTests
    {
        private readonly IScooterRepository _scooterRepository;
        private readonly GetScooterByIdQueryHandler _testee;
        private readonly List<Scooter> _scooters;
        private readonly int _scooterId = 100;

        public GetScooterByIdQueryHandlerTests()
        {
            _scooterRepository = A.Fake<IScooterRepository>();
            _testee = new GetScooterByIdQueryHandler(_scooterRepository);

            _scooters = new List<Scooter>
            {
                new()
                {
                    Id = 100,
                    ScooterId = _scooterId,
                    ChargePercent = 42
                },
                new()
                {
                    Id = 101,
                    ScooterId = _scooterId,
                    ChargePercent = 22
                }
            };
        }

        [Fact]
        public async Task Handle_ShouldReturnScootersByScooterId()
        {
            A.CallTo(() => _scooterRepository.GetScooterByIdAsync(_scooterId, default)).Returns(_scooters);

            var result = await _testee.Handle(new GetScooterByIdQuery { Id = _scooterId }, default);

            A.CallTo(() => _scooterRepository.GetScooterByIdAsync(_scooterId, default)).MustHaveHappenedOnceExactly();
            result.Should().BeOfType<List<Scooter>>();
            result.Count.Should().Be(2);
        }
    }
}