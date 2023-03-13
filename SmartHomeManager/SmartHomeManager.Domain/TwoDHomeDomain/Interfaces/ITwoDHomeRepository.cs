using SmartHomeManager.Domain.TwoDHomeDomain.Entities;

namespace SmartHomeManager.Domain.TwoDHomeDomain.Interfaces;

public interface ITwoDHomeRepository
{
    List<IRoomCoordinate> GetAllRoomCoordinatesRelatedToAccount(Guid accountId);
    Task AddRange(IEnumerable<IRoomCoordinate> entities);
    Task RemoveRange(IEnumerable<IRoomCoordinate> entities);

    // for testing purposes only
    Task<IEnumerable<DeviceCoordinate>> GetAllDeviceCoordinates();
    Task<IEnumerable<RoomCoordinate>> GetAllRoomCoordinates();
}