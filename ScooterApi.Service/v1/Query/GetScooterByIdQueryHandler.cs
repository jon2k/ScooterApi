using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ScooterApi.Data.Repository.v1;
using ScooterApi.Domain.Entities;

namespace ScooterApi.Service.v1.Query
{
    public class GetScooterByIdQueryHandler : IRequestHandler<GetScooterByIdQuery, List<Scooter>>
    {
        private readonly IScooterRepository _scooterRepository;

        public GetScooterByIdQueryHandler(IScooterRepository scooterRepository)
        {
            _scooterRepository = scooterRepository;
        }

        public async Task<List<Scooter>> Handle(GetScooterByIdQuery request, CancellationToken cancellationToken)
        {
            return await _scooterRepository.GetScooterByIdAsync(request.Id, cancellationToken);
        }
    }
}