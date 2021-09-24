using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ScooterApi.Data.Repository.v1;
using ScooterApi.Domain.Entities;
using ScooterApi.Messaging.Send.Sender.v1;

namespace ScooterApi.Service.v1.Command
{
    public class UpdateScooterCommandHandler : IRequestHandler<UpdateScooterCommand, Scooter>
    {
        private readonly IScooterRepository _scooterRepository;

        public UpdateScooterCommandHandler(IScooterRepository scooterRepository)
        {
            _scooterRepository = scooterRepository;
        }

        public async Task<Scooter> Handle(UpdateScooterCommand request, CancellationToken cancellationToken)
        {
            var scooter = await _scooterRepository.UpdateAsync(request.Scooter);
            return scooter;
        }
    }
}