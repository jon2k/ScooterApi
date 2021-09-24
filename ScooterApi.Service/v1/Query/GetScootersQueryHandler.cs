using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ScooterApi.Data.Repository.v1;
using ScooterApi.Domain.Entities;

namespace ScooterApi.Service.v1.Query
{
    public class GetScootersQueryHandler : IRequestHandler<GetScootersQuery, List<Scooter>>
    {
        private readonly IScooterRepository _scooterRepository;

        public GetScootersQueryHandler(IScooterRepository scooterRepository)
        {
            _scooterRepository = scooterRepository;
        }

        public async Task<List<Scooter>> Handle(GetScootersQuery request, CancellationToken cancellationToken)
        {
            return _scooterRepository.GetAll().ToList();
        }
    }
}