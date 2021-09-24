﻿using System.Collections.Generic;
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
            return await ScooterContext.Scooter.Where(x=>x.ScooterId==id)
                .ToListAsync(cancellationToken);
        }
    }
}