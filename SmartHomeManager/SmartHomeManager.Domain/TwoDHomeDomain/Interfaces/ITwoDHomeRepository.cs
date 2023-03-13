using SmartHomeManager.Domain.RoomDomain.Entities;
using SmartHomeManager.Domain.TwoDHomeDomain.Entities;

namespace SmartHomeManager.Domain.TwoDHomeDomain.Interfaces;

public interface ITwoDHomeRepository
{
    List<IRoomCoordinate> GetAllRoomCoordinatesRelatedToAccount(Guid accountId);
    Task<bool> AddRoomCoordinate(IRoomCoordinate roomCoordinate);
    Task<bool> AddDeviceCoordinate(IDeviceCoordinate deviceCoordinate);

    Task AddRange(IEnumerable<IRoomCoordinate> entities);
    Task RemoveRange(IEnumerable<IRoomCoordinate> entities);

    Task<DeviceCoordinate?> GetDeviceCoordinate(Guid id);
    Task<IEnumerable<DeviceCoordinate>> GetAllDeviceCoordinates();
    void UpdateDeviceCoordinate(DeviceCoordinate deviceCoordinate);
    void RemoveDeviceCoordinate(DeviceCoordinate deviceCoordinate);

    Task<RoomCoordinate?> GetRoomCoordinate(Guid id);
    Task<IEnumerable<RoomCoordinate>> GetAllRoomCoordinates();
    void UpdateRoomCoordinate(RoomCoordinate roomCoordinate);
    void RemoveRoomCoordinate(RoomCoordinate roomCoordinate);
    Task SaveChangesAsync();
}