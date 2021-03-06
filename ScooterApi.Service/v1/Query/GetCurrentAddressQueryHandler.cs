using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ScooterApi.Address.Yandex.Yandex.v1;
using ScooterApi.Data.Repository.v1;

namespace ScooterApi.Service.v1.Query;

public class GetCurrentAddressQueryHandler: IRequestHandler<GetCurrentAddressQuery, Domain.Entities.Address.Address>
{
    private readonly IScooterRepository _scooterRepository;
    private readonly IAddressService _addressService;
    public GetCurrentAddressQueryHandler(IScooterRepository scooterRepository, IAddressService addressService)
    {
        _scooterRepository = scooterRepository;
        _addressService = addressService;
    }

    public async Task<Domain.Entities.Address.Address> Handle(GetCurrentAddressQuery request, CancellationToken cancellationToken)
    {
        var coordinate = await _scooterRepository.GetLastCoordinateAsync(request.Id, cancellationToken);
        return await _addressService.GetAddressAsync(coordinate, cancellationToken);
    }
}