using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.RoomDomain.DTOs.Responses;
using SmartHomeManager.Domain.RoomDomain.Entities;
using SmartHomeManager.Domain.RoomDomain.Factories;
using SmartHomeManager.Domain.RoomDomain.Interfaces;
using SmartHomeManager.Domain.RoomDomain.Mocks;

namespace SmartHomeManager.Domain.RoomDomain.Services;

public class RoomReadService : IRoomReadService, IRoomInformationService
{
    private readonly IDeviceInformationServiceMock _deviceInformationService;
    private readonly IRoomRepository _roomRepository;

    public RoomReadService(IRoomRepository roomRepository, IDeviceInformationServiceMock deviceInformationService)
    {
        _roomRepository = roomRepository;
        _deviceInformationService = deviceInformationService;
    }

    public IList<IRoom> GetRoomsByAccountId(Guid accountId)
    {
        return _roomRepository.GetRoomsRelatedToAccount(accountId).ToList();
    }

    public async Task<IRoomWebResponse?> GetRoomById(Guid roomId)
    {
        var res = await _roomRepository.Get(roomId);
        if (res == null) return null;
        return RoomWebResponseFactory.CreateRoomWebResponse(res.RoomId, res.Name, res.AccountId);
    }

    public async Task<IEnumerable<IRoomWebResponse>> GetAllRooms()
    {
        var result = await _roomRepository.GetAll();
        var resp = result.Select(room =>
            RoomWebResponseFactory.CreateRoomWebResponse(room.RoomId, room.Name, room.AccountId)
        ).ToList();
        return resp;
    }

    public IEnumerable<Device> GetDevicesInRoom(Guid roomId)
    {
        return _deviceInformationService.GetDevicesInRoom(roomId);
    }

    // IList allows for more direct manipulation, so IEnumerable is used instead
    public IEnumerable<IRoomWebResponse> GetRoomsRelatedToAccount(Guid accountId)
    {
        var result = _roomRepository.GetRoomsRelatedToAccount(accountId);
        var resp = result.Select(room =>
            RoomWebResponseFactory.CreateRoomWebResponse(room.RoomId, room.Name, room.AccountId)
        ).ToList();
        return resp;
    }
}