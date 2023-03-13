using SmartHomeManager.Domain.TwoDHomeDomain.DTOs.Responses;
using SmartHomeManager.Domain.TwoDHomeDomain.Entities;
using SmartHomeManager.Domain.TwoDHomeDomain.Factories;
using SmartHomeManager.Domain.TwoDHomeDomain.Interfaces;
using SmartHomeManager.Domain.TwoDHomeDomain.Mocks;

namespace SmartHomeManager.Domain.TwoDHomeDomain.Services;

public class TwoDHomeWriteService : ITwoDHomeWriteService
{
    private readonly ITwoDHomeRepository _twoDHomeRepository;
    private readonly IControlDeviceServiceMock _updateDeviceService;

    public TwoDHomeWriteService(ITwoDHomeRepository twoDHomeRepository, IControlDeviceServiceMock updateDeviceService)
    {
        _twoDHomeRepository = twoDHomeRepository;
        _updateDeviceService = updateDeviceService;
    }

    public ITwoDHomeWebResponse UpdateRoomGrids(Guid accountId, List<RoomGrid> roomGrids)
    {
        var allRoomCoordinates = _twoDHomeRepository.GetAllRoomCoordinatesRelatedToAccount(accountId);
        _twoDHomeRepository.RemoveRange(allRoomCoordinates);

        var updatedRoomCoordinates = new List<IRoomCoordinate>();
        foreach (var room in roomGrids)
            updatedRoomCoordinates.Add(RoomCoordinateFactory.CreateRoomCoordinate
                (
                    room.XCoordinate,
                    room.YCoordinate,
                    room.Width,
                    room.Height,
                    room.RoomId
                )
            );
        _twoDHomeRepository.AddRange(updatedRoomCoordinates);
        return TwoDHomeWebResponseFactory.CreateRoomWebResponse(roomGrids);
    }

    public bool ChangeDeviceState(Guid deviceId, bool state)
    {
        if (state)
            _updateDeviceService.SwitchOnDevice(deviceId);
        else
            _updateDeviceService.SwitchOffDevice(deviceId);
        return true;
    }
}