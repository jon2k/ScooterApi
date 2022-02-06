using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ScooterApi.Data.Repository.v1;
using ScooterApi.Domain.Entities;
using ScooterApi.Messaging.Send.Sender.v1;

namespace ScooterApi.Service.v1.Command
{
    public class CreateScooterCommandHandler : IRequestHandler<CreateScooterCommand, Scooter>
    {
        private readonly IScooterRepository _scooterRepository;
        private readonly IScooterAddSender _scooterAddSender;

        public CreateScooterCommandHandler(IScooterAddSender scooterAddSender,IScooterRepository scooterRepository)
        {
            _scooterRepository = scooterRepository;
            _scooterAddSender = scooterAddSender;
        }

        public async Task<Scooter> Handle(CreateScooterCommand request, CancellationToken cancellationToken)
        {
            request.Scooter.Id=Guid.NewGuid();
            var result = await _scooterRepository.AddAsync(request.Scooter);
            _scooterAddSender.SendScooter(result);
            return result;
        }
    }
}