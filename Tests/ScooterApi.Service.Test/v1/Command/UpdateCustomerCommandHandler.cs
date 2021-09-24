using FakeItEasy;
using FluentAssertions;
using ScooterApi.Data.Repository.v1;
using ScooterApi.Domain.Entities;
using ScooterApi.Messaging.Send.Sender.v1;
using ScooterApi.Service.v1.Command;
using Xunit;

namespace ScooterApi.Service.Test.v1.Command
{
    public class UpdateScooterCommandHandlerTests
    {
        private readonly UpdateScooterCommandHandler _testee;
        private readonly IScooterRepository _scooterRepository;
        private readonly Scooter _scooter;

        public UpdateScooterCommandHandlerTests()
        {
            _scooterRepository = A.Fake<IScooterRepository>();
            _testee = new UpdateScooterCommandHandler( _scooterRepository);

            _scooter = new Scooter
            {
                ChargePercent = 30
            };
        }

        [Fact]
        public async void Handle_ShouldReturnUpdatedScooter()
        {
            A.CallTo(() => _scooterRepository.UpdateAsync(A<Scooter>._)).Returns(_scooter);

            var result = await _testee.Handle(new UpdateScooterCommand(), default);

            result.Should().BeOfType<Scooter>();
            result.ChargePercent.Should().Be(_scooter.ChargePercent);
        }

        [Fact]
        public async void Handle_ShouldUpdateAsync()
        {
            await _testee.Handle(new UpdateScooterCommand(), default);

            A.CallTo(() => _scooterRepository.UpdateAsync(A<Scooter>._)).MustHaveHappenedOnceExactly();
        }
    }
}