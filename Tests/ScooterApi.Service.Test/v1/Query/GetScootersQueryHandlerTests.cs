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
    public class GetScootersQueryHandlerTests
    {
        private readonly IScooterRepository _scooterRepository;
        private readonly GetScootersQueryHandler _testee;
        private readonly List<Scooter> _scooters;

        public GetScootersQueryHandlerTests()
        {
            _scooterRepository = A.Fake<IScooterRepository>();
            _testee = new GetScootersQueryHandler(_scooterRepository);

            _scooters = new List<Scooter>
            {
                new()
                {
                    Id = 100,
                    ChargePercent = 42
                },
                new()
                {
                    Id = 101,
                    ChargePercent = 22
                }
            };
        }

        [Fact]
        public async Task Handle_ShouldReturnScooters()
        {
            A.CallTo(() => _scooterRepository.GetAll()).Returns(_scooters);

            var result = await _testee.Handle(new GetScootersQuery(), default);

            A.CallTo(() => _scooterRepository.GetAll()).MustHaveHappenedOnceExactly();
            result.Should().BeOfType<List<Scooter>>();
            result.Count.Should().Be(2);
        }
    }
}