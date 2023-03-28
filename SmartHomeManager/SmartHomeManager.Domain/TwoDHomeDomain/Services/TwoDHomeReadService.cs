using SmartHomeManager.Domain.RoomDomain.Interfaces;
using SmartHomeManager.Domain.RoomDomain.Mocks;
using SmartHomeManager.Domain.TwoDHomeDomain.DTOs.Responses;
using SmartHomeManager.Domain.TwoDHomeDomain.Entities;
using SmartHomeManager.Domain.TwoDHomeDomain.Factories;
using SmartHomeManager.Domain.TwoDHomeDomain.Interfaces;

namespace SmartHomeManager.Domain.TwoDHomeDomain.Services;

public class TwoDHomeReadService : ITwoDHomeReadService
{
    private readonly IDeviceInformationServiceMock _deviceInformationService;
    private readonly IRoomRepository _roomRepository;
    private readonly ITwoDHomeRepository _twoDHomeRepository;

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
            return TwoDHomeWebResponseFactory.CreateRoomWebResponse(new List<RoomGrid>());

        // if there are room coordinates, then there are rooms due to database constraints
        var allRooms = _roomRepository.GetRoomsRelatedToAccount(accountId).ToList();
        var allRoomGrids = new List<RoomGrid>();
        foreach (var room in allRooms)
        {
            // this handles the case where the room exists but do not have a room coordinate
            if (room.RoomCoordinate == null)
            {
                allRoomGrids.Add(RoomGridFactory.CreateEmptyRoomGrid(room));
                continue;
            }

            var devicesInRoom = _deviceInformationService.GetDevicesInRoom(room.RoomId).ToList();
            room.Devices = devicesInRoom;
            var deviceStateMapping = new Dictionary<Guid, bool>();
            foreach (var device in room.Devices)
                deviceStateMapping.Add(device.DeviceId, _deviceInformationService.isDeviceOn(device.DeviceId));
            allRoomGrids.Add(RoomGridFactory.CreateRoomGrid(room, deviceStateMapping));
        }

        return TwoDHomeWebResponseFactory.CreateRoomWebResponse(allRoomGrids);
    }

    public async Task<IRoomCoordinateResponse?> GetRoomCoordinate(Guid roomCoordinateId)
    {
        var res = await _twoDHomeRepository.GetRoomCoordinateById(roomCoordinateId);
        if (res == null) return null;
        return RoomCoordinateResponseFactory.CreateRoomCoordinateResponse(res);
    }

    public async Task<IDeviceCoordinateResponse?> GetDeviceCoordinate(Guid deviceCoordinateId)
    {
        var res = await _twoDHomeRepository.GetDeviceCoordinateById(deviceCoordinateId);
        if (res == null) return null;
        return DeviceCoordinateResponseFactory.CreateDeviceCoordinateResponse(res);
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