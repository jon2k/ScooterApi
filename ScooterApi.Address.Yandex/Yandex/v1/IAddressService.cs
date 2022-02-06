using ScooterApi.Domain.Entities;
using ScooterApi.Domain.Entities.Address;

namespace ScooterApi.Address.Yandex.Yandex.v1;

public interface IAddressService
{
    Task<Domain.Entities.Address.Address> GetAddressAsync(Coordinate coordinate, CancellationToken cancellationToken);
}