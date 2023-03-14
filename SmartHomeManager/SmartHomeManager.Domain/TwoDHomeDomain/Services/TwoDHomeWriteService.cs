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
                    room.x,
                    room.y,
                    room.w,
                    room.h,
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
        // validation: roomId must refer to an existing room in order to create a roomCoordinate
        var room = await _roomRepository.Get(roomId);
        if (room == null) return null;

        // validation: roomCoordinate must not exist in order to create a new one 
        var roomCoordinate = _twoDHomeRepository.GetRoomCoordinateByRoomId(roomId);
        if (roomCoordinate != null) return null;
        var newRoomCoordinate = RoomCoordinateFactory.CreateRoomCoordinate(new Guid(), x, y, w, h, roomId);
        await _twoDHomeRepository.AddRoomCoordinate(newRoomCoordinate);
        return RoomCoordinateResponseFactory.CreateRoomCoordinateResponse(newRoomCoordinate);
    }

    public async Task<IDeviceCoordinateResponse?> AddDeviceCoordinate(int x, int y, int w, int h, Guid deviceId)
    {
        // validation: deviceId must refer to an existing device in order to create a deviceCoordinate
        var device = _deviceInformationService.GetDeviceById(deviceId);
        if (device == null) return null;

        // validation: deviceCoordinate must not exist in order to create a new one
        var deviceCoordinate = _twoDHomeRepository.GetDeviceCoordinateByDeviceId(deviceId);
        if (deviceCoordinate != null) return null;
        var newDeviceCoordinate = DeviceCoordinateFactory.CreateDeviceCoordinate(new Guid(), x, y, w, h, deviceId);
        await _twoDHomeRepository.AddDeviceCoordinate(newDeviceCoordinate);
        return DeviceCoordinateResponseFactory.CreateDeviceCoordinateResponse(newDeviceCoordinate);
    }
}