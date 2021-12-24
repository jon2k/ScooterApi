using MediatR;
using ScooterApi.Domain.Entities.Address;

namespace ScooterApi.Service.v1.Query;

public class GetCurrentAddressQuery : IRequest<AddressScooter>
{
    public int Id { get; set; }
}