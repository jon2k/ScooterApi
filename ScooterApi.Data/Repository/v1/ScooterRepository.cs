using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ScooterApi.Data.Database;
using ScooterApi.Domain.Entities;

namespace ScooterApi.Data.Repository.v1
{
    public class ScooterRepository : Repository<Scooter>, IScooterRepository
    {
        public ScooterRepository(ScooterContext scooterContext) : base(scooterContext)
        {
        }

        public async Task<List<Scooter>> GetScooterByIdAsync(int id, CancellationToken cancellationToken)
        {
            var scooters= await ScooterContext.Scooter.Where(x=>x.ScooterId==id)
                .ToListAsync(cancellationToken);
            if (scooters.Count==0)
            {
                throw new Exception($"No entries for scooter {id}");
            }

            return scooters;
        }

        public async Task<Coordinate> GetLastCoordinateAsync(int id, CancellationToken cancellationToken)
        {
            var scooter= await ScooterContext.Scooter
                .Where(s=>s.ScooterId==id)
                . OrderByDescending(s => s.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (scooter==null)
            {
                throw new Exception("Сould not find entry");
            }
            return new Coordinate(){X = scooter.CoordinateX, Y = scooter.CoordinateY};
        }
    }
}