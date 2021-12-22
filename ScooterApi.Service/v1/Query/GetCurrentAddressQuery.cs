using MediatR;

namespace ScooterApi.Service.v1.Query;

public class GetCurrentAddressQuery : IRequest<Domain.Entities.Address.Address>
{
    public int Id { get; set; }
}