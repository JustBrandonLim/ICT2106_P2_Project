using System.Collections;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.RoomDomain.Entities;
using SmartHomeManager.Domain.RoomDomain.Interfaces;
using SmartHomeManager.Domain.RoomDomain.Mocks;
using SmartHomeManager.Domain.TwoDHomeDomain.DTOs.Responses;
using SmartHomeManager.Domain.TwoDHomeDomain.Entities;
using SmartHomeManager.Domain.TwoDHomeDomain.Factories;
using SmartHomeManager.Domain.TwoDHomeDomain.Interfaces;

namespace SmartHomeManager.Domain.TwoDHomeDomain.Services;

public class TwoDHomeReadService : ITwoDHomeReadService
{
    private readonly ITwoDHomeRepository _twoDHomeRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly IDeviceInformationServiceMock _deviceInformationService;

    public TwoDHomeReadService(
        ITwoDHomeRepository twoDHomeRepository,
        IRoomRepository roomRepository,
        IDeviceInformationServiceMock deviceInformationService
    )
    {
        _twoDHomeRepository = twoDHomeRepository;
        _roomRepository = roomRepository;
        _deviceInformationService = deviceInformationService;
    }

    public ITwoDHomeWebResponse GetAllRoomGridsRelatedToAccount(Guid accountId)
    {
        var allRoomCoordinatesList = _twoDHomeRepository.GetAllRoomCoordinatesRelatedToAccount(accountId);
        if (!allRoomCoordinatesList.Any())
            return TwoDHomeWebResponseFactory.CreateRoomWebResponse(new List<IRoomGrid>());

        var allRooms = _roomRepository.GetRoomsRelatedToAccount(accountId).ToList();
        var allRoomGrids = new List<IRoomGrid>();
        foreach (var room in allRooms)
        {
            // ignore warning, this handles the case where the room exists but do not have a room coordinate
            if (room.RoomCoordinate == null) continue;
            var devicesInRoom = _deviceInformationService.GetDevicesInRoom(room.RoomId).ToList();
            room.Devices = devicesInRoom;
            var deviceStateMapping = new Dictionary<Guid, bool>();
            foreach (var device in room.Devices)
                deviceStateMapping.Add(device.DeviceId, _deviceInformationService.isDeviceOn(device.DeviceId));
            allRoomGrids.Add(RoomGridFactory.CreateRoomGrid(room, deviceStateMapping));
        }

        return TwoDHomeWebResponseFactory.CreateRoomWebResponse(allRoomGrids);
    }

    // for testing purposes only
    public async Task<IEnumerable<IDeviceCoordinate>> GetAllDeviceCoordinates()
    {
        var result = await _twoDHomeRepository.GetAllDeviceCoordinates();
        return result;
    }

    public async Task<IEnumerable<IRoomCoordinate>> GetAllRoomCoordinates()
    {
        var result = await _twoDHomeRepository.GetAllRoomCoordinates();
        return result;
    }
}