using SmartHomeManager.Domain.TwoDHomeDomain.DTOs.Requests;
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
        _twoDHomeRepository.RemoveRange(_twoDHomeRepository.GetAllRoomCoordinatesRelatedToAccount(accountId));

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

    public async Task UpdateDeviceCoordinate(Guid deviceCoordinateId, int xCoordinate, int yCoordinate, int height,
        int width)
    {
        var res = await _twoDHomeRepository.GetDeviceCoordinate(deviceCoordinateId);
        if (res == null) return;
        res.XCoordinate = xCoordinate;
        res.YCoordinate = yCoordinate;
        res.Height = height;
        res.Width = width;
        _twoDHomeRepository.UpdateDeviceCoordinate(res);
        await _twoDHomeRepository.SaveChangesAsync();
    }

    public async Task RemoveDeviceCoordinate(Guid id)
    {
        var res = await _twoDHomeRepository.GetDeviceCoordinate(id);
        if (res == null) return;
        _twoDHomeRepository.RemoveDeviceCoordinate(res);
        await _twoDHomeRepository.SaveChangesAsync();
    }

    public async Task RemoveRoomCoordinate(Guid id)
    {
        var res = await _twoDHomeRepository.GetRoomCoordinate(id);
        if (res == null) return;
        _twoDHomeRepository.RemoveRoomCoordinate(res);
        await _twoDHomeRepository.SaveChangesAsync();
    }
}