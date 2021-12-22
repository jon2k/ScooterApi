using ScooterApi.Domain.Entities;

namespace ScooterApi.Address.Yandex.Yandex.v1;

public interface IGetAddress
{
    Task<Address> GetAddressAsync(Coordinate coordinate, CancellationToken cancellationToken);
}