using SmartHomeManager.Domain.RoomDomain.DTOs.Responses;

namespace SmartHomeManager.Domain.RoomDomain.Factories;

public class RoomWebResponseFactory
{
    public static IRoomWebResponse CreateRoomWebResponse(Guid roomId, string name, Guid accountId)
    {
        return new RoomWebResponse
        {
            RoomId = roomId,
            Name = name,
            AccountId = accountId
        };
    }
}