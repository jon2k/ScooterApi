using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Azure;
using ScooterApi.Data.Repository.v1;

namespace ScooterApi.Service.v1.Query;

public class GetCurrentAddressQueryHandler: IRequestHandler<GetCurrentAddressQuery, Address>
{
    private readonly IScooterRepository _scooterRepository;
    private readonly IGetAddress _getAddress;
    public GetCurrentAddressQueryHandler(IScooterRepository scooterRepository, IGetAddress getAddress)
    {
        _scooterRepository = scooterRepository;
        _getAddress = getAddress;
    }

    public async Task<Address> Handle(GetCurrentAddressQuery request, CancellationToken cancellationToken)
    {
        var coordinate = await _scooterRepository.GetLastCoordinateAsync(cancellationToken);
        return await _getAddress.AddServiceBusClient(coordinate);
    }
}