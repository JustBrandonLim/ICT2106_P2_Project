using SmartHomeManager.Domain.RoomDomain.Interfaces;
using SmartHomeManager.Domain.RoomDomain.Mocks;
using SmartHomeManager.Domain.TwoDHomeDomain.DTOs.Responses;
using SmartHomeManager.Domain.TwoDHomeDomain.Entities;
using SmartHomeManager.Domain.TwoDHomeDomain.Factories;
using SmartHomeManager.Domain.TwoDHomeDomain.Interfaces;
using SmartHomeManager.Domain.TwoDHomeDomain.Mocks;

namespace SmartHomeManager.Domain.TwoDHomeDomain.Services;

public class TwoDHomeWriteService : ITwoDHomeWriteService
{
    private readonly IDeviceInformationServiceMock _deviceInformationService;
    private readonly IRoomRepository _roomRepository;
    private readonly ITwoDHomeRepository _twoDHomeRepository;
    private readonly IControlDeviceServiceMock _updateDeviceService;

    public TwoDHomeWriteService(ITwoDHomeRepository twoDHomeRepository, IControlDeviceServiceMock updateDeviceService,
        IRoomRepository roomRepository, IDeviceInformationServiceMock deviceInformationService)
    {
        _twoDHomeRepository = twoDHomeRepository;
        _updateDeviceService = updateDeviceService;
        _roomRepository = roomRepository;
        _deviceInformationService = deviceInformationService;
    }

    public ITwoDHomeWebResponse UpdateRoomGrids(Guid accountId, List<RoomGrid> roomGrids)
    {
        var allRoomCoordinates = _twoDHomeRepository.GetAllRoomCoordinatesRelatedToAccount(accountId);
        _twoDHomeRepository.RemoveRange(allRoomCoordinates);

        var updatedRoomCoordinates = new List<IRoomCoordinate>();
        foreach (var room in roomGrids)
            updatedRoomCoordinates.Add(RoomCoordinateFactory.CreateRoomCoordinate
                (
                    room.RoomCoordinateId,
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

    public async Task<IRoomCoordinateResponse?> AddRoomCoordinate(int x, int y, int w, int h, Guid roomId)
    {
        var room = await _roomRepository.Get(roomId);
        if (room == null) return null;
        var roomCoordinate = _twoDHomeRepository.GetRoomCoordinateByRoomId(roomId);
        if (roomCoordinate != null) return null;
        var newRoomCoordinate = RoomCoordinateFactory.CreateRoomCoordinate(new Guid(), x, y, w, h, roomId);
        await _twoDHomeRepository.AddRoomCoordinate(newRoomCoordinate);
        return RoomCoordinateResponseFactory.CreateRoomCoordinateResponse(newRoomCoordinate);
    }

    public async Task<IDeviceCoordinateResponse?> AddDeviceCoordinate(int x, int y, int w, int h, Guid deviceId)
    {
        var device = _deviceInformationService.GetDeviceById(deviceId);
        if (device == null) return null;
        var deviceCoordinate = _twoDHomeRepository.GetDeviceCoordinateByDeviceId(deviceId);
        if (deviceCoordinate != null) return null;
        var newDeviceCoordinate = DeviceCoordinateFactory.CreateDeviceCoordinate(new Guid(), x, y, w, h, deviceId);
        await _twoDHomeRepository.AddDeviceCoordinate(newDeviceCoordinate);
        return DeviceCoordinateResponseFactory.CreateDeviceCoordinateResponse(newDeviceCoordinate);
    }
}