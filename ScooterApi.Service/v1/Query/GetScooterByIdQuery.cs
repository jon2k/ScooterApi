using System;
using System.Collections.Generic;
using MediatR;
using ScooterApi.Domain.Entities;

namespace ScooterApi.Service.v1.Query
{
    public class GetScooterByIdQuery : IRequest<List<Scooter>>
    {
        public int Id { get; set; }
    }
}