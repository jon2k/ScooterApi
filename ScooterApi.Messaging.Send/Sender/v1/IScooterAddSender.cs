using ScooterApi.Domain.Entities;

namespace ScooterApi.Messaging.Send.Sender.v1
{
    public interface IScooterAddSender
    {
        void SendScooter(Scooter scooter);
    }
}