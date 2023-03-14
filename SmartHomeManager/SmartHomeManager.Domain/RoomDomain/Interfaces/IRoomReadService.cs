using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.RoomDomain.DTOs.Responses;
using SmartHomeManager.Domain.RoomDomain.Entities;

namespace SmartHomeManager.Domain.RoomDomain.Interfaces;

public interface IRoomReadService
{
    Task<IEnumerable<IRoomWebResponse>> GetAllRooms();
    IEnumerable<Device> GetDevicesInRoom(Guid roomId);
    Task<IRoomWebResponse?> GetRoomById(Guid roomId);
    IEnumerable<IRoomWebResponse> GetRoomsRelatedToAccount(Guid accountId);
}