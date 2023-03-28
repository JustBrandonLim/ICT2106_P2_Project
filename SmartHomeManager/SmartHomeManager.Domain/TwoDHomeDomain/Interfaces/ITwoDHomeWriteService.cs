using SmartHomeManager.Domain.TwoDHomeDomain.DTOs.Responses;
using SmartHomeManager.Domain.TwoDHomeDomain.Entities;

namespace SmartHomeManager.Domain.TwoDHomeDomain.Interfaces;

public interface ITwoDHomeWriteService
{
    ITwoDHomeWebResponse UpdateRoomGrids(Guid accountId, List<RoomGrid> roomGrids);
    bool ChangeDeviceState(Guid deviceId, bool state);
    Task<IRoomCoordinateResponse?> AddRoomCoordinate(int x, int y, int w, int h, Guid roomId);
    Task<IDeviceCoordinateResponse?> AddDeviceCoordinate(int x, int y, int w, int h, Guid deviceId);
}