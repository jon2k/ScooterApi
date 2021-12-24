using System.Globalization;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ScooterApi.Address.Yandex.Options.v1;
using ScooterApi.Domain.Entities;
using ScooterApi.Domain.Entities.Address;

namespace ScooterApi.Address.Yandex.Yandex.v1;

public class AddressService : IAddressService
{
    private readonly HttpClient _httpClient;
    private readonly string _uri;
    private readonly string _keyApiYandexMaps;

    public AddressService(HttpClient httpClient, IOptions<YandexMapApiConfiguration> yandexMapOptions)
    {
        _httpClient = httpClient;
        _uri = yandexMapOptions.Value.Uri;
        _keyApiYandexMaps = yandexMapOptions.Value.KeyApiYandexMaps;
    }

    public async Task<AddressScooter> GetAddressAsync(Coordinate coordinate, CancellationToken cancellationToken)
    {
        var uri = $"{_uri}{_keyApiYandexMaps}&geocode={coordinate.X.ToString(CultureInfo.InvariantCulture)},{coordinate.Y.ToString(CultureInfo.InvariantCulture)}";
        var responseString = await _httpClient.GetStringAsync(uri, cancellationToken);
        var address = JsonConvert.DeserializeObject<AddressScooter>(responseString);
        return address;
    }
}