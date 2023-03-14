using SmartHomeManager.Domain.RoomDomain.DTOs.Responses;

namespace SmartHomeManager.Domain.RoomDomain.Interfaces;

public interface IRoomWriteService
{
    Task<IRoomWebResponse> AddRoom(string name, Guid accountId);
    Task RemoveRoom(Guid roomId);
    Task UpdateRoom(Guid roomId, string name);
}