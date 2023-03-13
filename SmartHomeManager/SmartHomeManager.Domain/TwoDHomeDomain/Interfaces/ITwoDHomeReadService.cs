using SmartHomeManager.Domain.TwoDHomeDomain.DTOs.Responses;
using SmartHomeManager.Domain.TwoDHomeDomain.Entities;

namespace SmartHomeManager.Domain.TwoDHomeDomain.Interfaces;

public interface ITwoDHomeReadService
{
    ITwoDHomeWebResponse GetAllRoomGridsRelatedToAccount(Guid accountId);

    // for testing purposes only
    Task<IEnumerable<IDeviceCoordinate>> GetAllDeviceCoordinates();
    Task<IEnumerable<IRoomCoordinate>> GetAllRoomCoordinates();
}