using SmartHomeManager.Domain.RoomDomain.Entities;

namespace SmartHomeManager.Domain.RoomDomain.Interfaces;

public interface IRoomInformationService
{
    IList<Room> GetRoomsByAccountId(Guid accountId);
}