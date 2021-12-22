using MediatR;
using Microsoft.Azure.Amqp.Framing;

namespace ScooterApi.Service.v1.Query;

public class GetCurrentAddressQuery: IRequest<Address>
{
    
}