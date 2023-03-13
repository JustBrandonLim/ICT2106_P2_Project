using SmartHomeManager.Domain.TwoDHomeDomain.DTOs.Requests;
using SmartHomeManager.Domain.TwoDHomeDomain.DTOs.Responses;
using SmartHomeManager.Domain.TwoDHomeDomain.Entities;

namespace SmartHomeManager.Domain.TwoDHomeDomain.Interfaces;

public interface ITwoDHomeWriteService
{
    ITwoDHomeWebResponse UpdateRoomGrids(Guid accountId, List<RoomGrid> roomGrids);

    bool ChangeDeviceState(Guid deviceId, bool state);
}