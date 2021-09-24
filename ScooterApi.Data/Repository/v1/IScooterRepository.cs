using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ScooterApi.Domain.Entities;

namespace ScooterApi.Data.Repository.v1
{
    public interface IScooterRepository: IRepository<Scooter>
    {
        Task<List<Scooter>> GetScooterByIdAsync(int id, CancellationToken cancellationToken);
    }
}