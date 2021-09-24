using FakeItEasy;
using FluentAssertions;
using ScooterApi.Data.Repository.v1;
using ScooterApi.Domain.Entities;
using ScooterApi.Messaging.Send.Sender.v1;
using ScooterApi.Service.v1.Command;
using Xunit;

namespace ScooterApi.Service.Test.v1.Command
{
    public class CreateScooterCommandHandlerTests
    {
        private readonly CreateScooterCommandHandler _testee;
        private readonly IScooterRepository _scooterRepository;
        private readonly IScooterAddSender _scooterAddSender;

        public CreateScooterCommandHandlerTests()
        {
            _scooterRepository = A.Fake<IScooterRepository>();
            _scooterAddSender = A.Fake<IScooterAddSender>();
            _testee = new CreateScooterCommandHandler(_scooterAddSender,_scooterRepository);
        }

        [Fact]
        public async void Handle_ShouldCallAddAsync()
        {
            await _testee.Handle(new CreateScooterCommand(), default);

            A.CallTo(() => _scooterRepository.AddAsync(A<Scooter>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void Handle_ShouldReturnCreatedScooter()
        {
            A.CallTo(() => _scooterRepository.AddAsync(A<Scooter>._)).Returns(new Scooter
            {
                ChargePercent = 15
            });

            var result = await _testee.Handle(new CreateScooterCommand(), default);

            result.Should().BeOfType<Scooter>();
            result.ChargePercent.Should().Be(15);
        }
    }
}