using MediatR;
using ScooterApi.Domain.Entities;

namespace ScooterApi.Service.v1.Command
{
    public class CreateScooterCommand : IRequest<Scooter>
    {
        public Scooter Scooter { get; set; }
    }
}