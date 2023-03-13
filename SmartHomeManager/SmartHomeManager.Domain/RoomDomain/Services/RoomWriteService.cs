using SmartHomeManager.Domain.RoomDomain.DTOs.Responses;
using SmartHomeManager.Domain.RoomDomain.Factories;
using SmartHomeManager.Domain.RoomDomain.Interfaces;

namespace SmartHomeManager.Domain.RoomDomain.Services;

public class RoomWriteService : IRoomWriteService
{
    private readonly IRoomRepository _roomRepository;

    public RoomWriteService(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task<IRoomWebResponse> AddRoom(string name, Guid accountId)
    {
        var newRoom = RoomFactory.CreateRoom(name, accountId);
        await _roomRepository.Add(newRoom);
        return RoomWebResponseFactory.CreateRoomWebResponse(newRoom.RoomId, newRoom.Name, newRoom.AccountId);
    }

    public async Task RemoveRoom(Guid roomId)
    {
        var res = await _roomRepository.Get(roomId);
        if (res == null) return;
        await _roomRepository.Remove(res);
    }

    public async Task UpdateRoom(Guid roomId, string name)
    {
        var res = await _roomRepository.Get(roomId);
        if (res == null) return;
        res.Name = name;
        await _roomRepository.Update(res);
    }
}