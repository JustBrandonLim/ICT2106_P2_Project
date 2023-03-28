using SmartHomeManager.Domain.RoomDomain.Entities;

namespace SmartHomeManager.Domain.RoomDomain.Factories;

public class RoomFactory
{
    public static IRoom CreateRoom(string name, Guid accountId)
    {
        return new Room
        {
            RoomId = Guid.NewGuid(),
            Name = name,
            AccountId = accountId
        };
    }
}