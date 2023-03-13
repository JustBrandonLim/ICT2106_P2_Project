using SmartHomeManager.Domain.RoomDomain.Entities;
using SmartHomeManager.Domain.TwoDHomeDomain.Entities;

namespace SmartHomeManager.Domain.TwoDHomeDomain.Interfaces;

public interface ITwoDHomeRepository
{
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