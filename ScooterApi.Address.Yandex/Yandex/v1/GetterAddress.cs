using Newtonsoft.Json;
using ScooterApi.Domain.Entities;

namespace ScooterApi.Address.Yandex.Yandex.v1;

public class GetterAddress:IGetAddress
{
    private readonly HttpClient _httpClient;
    public GetterAddress(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<Address> GetAddressAsync(Coordinate coordinate, CancellationToken cancellationToken)
    {
        var responseString = await _httpClient.GetStringAsync(uri);
        var address = JsonConvert.DeserializeObject<Address>(responseString);
        return address;
    }
}