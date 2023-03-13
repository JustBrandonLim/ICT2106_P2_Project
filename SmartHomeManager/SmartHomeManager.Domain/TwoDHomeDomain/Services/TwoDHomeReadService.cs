using SmartHomeManager.Domain.RoomDomain.Entities;
using SmartHomeManager.Domain.TwoDHomeDomain.Entities;
using SmartHomeManager.Domain.TwoDHomeDomain.Interfaces;

namespace SmartHomeManager.Domain.TwoDHomeDomain.Services;

public class TwoDHomeReadService
{
    private readonly ITwoDHomeRepository _twoDHomeRepository;

    public TwoDHomeReadService(ITwoDHomeRepository twoDHomeRepository)
    {
        _twoDHomeRepository = twoDHomeRepository;
    }

    public async Task<DeviceCoordinate?> GetDeviceCoordinate(Guid id)
    {
        var result = await _twoDHomeRepository.GetDeviceCoordinate(id);
        return result;
    }

    public async Task<IEnumerable<DeviceCoordinate>> GetAllDeviceCoordinates()
    {
        var result = await _twoDHomeRepository.GetAllDeviceCoordinates();
        return result;
    }

    public async Task<RoomCoordinate?> GetRoomCoordinate(Guid id)
    {
        var result = await _twoDHomeRepository.GetRoomCoordinate(id);
        return result;
    }

    public async Task<IEnumerable<RoomCoordinate>> GetAllRoomCoordinates()
    {
        var result = await _twoDHomeRepository.GetAllRoomCoordinates();
        return result;
    }
}