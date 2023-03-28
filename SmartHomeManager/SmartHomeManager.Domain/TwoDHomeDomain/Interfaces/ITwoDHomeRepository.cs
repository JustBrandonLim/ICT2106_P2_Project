using SmartHomeManager.Domain.TwoDHomeDomain.Entities;

namespace SmartHomeManager.Domain.TwoDHomeDomain.Interfaces;

public interface ITwoDHomeRepository
{
    List<IRoomCoordinate> GetAllRoomCoordinatesRelatedToAccount(Guid accountId);
    Task AddRange(IEnumerable<IRoomCoordinate> entities);
    Task RemoveRange(IEnumerable<IRoomCoordinate> entities);
    Task AddRoomCoordinate(IRoomCoordinate entity);
    Task<IRoomCoordinate?> GetRoomCoordinateById(Guid roomCoordinateId);
    IRoomCoordinate? GetRoomCoordinateByRoomId(Guid roomId);
    Task AddDeviceCoordinate(IDeviceCoordinate entity);
    Task<IDeviceCoordinate?> GetDeviceCoordinateById(Guid deviceCoordinateId);
    IDeviceCoordinate? GetDeviceCoordinateByDeviceId(Guid deviceId);

    // for testing purposes only
    Task<IEnumerable<IDeviceCoordinate>> GetAllDeviceCoordinates();
    Task<IEnumerable<IRoomCoordinate>> GetAllRoomCoordinates();
}