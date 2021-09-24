using System.Collections.Generic;
using MediatR;
using ScooterApi.Domain.Entities;

namespace ScooterApi.Service.v1.Query
{
    public class GetScootersQuery : IRequest<List<Scooter>>
    {
    }
}