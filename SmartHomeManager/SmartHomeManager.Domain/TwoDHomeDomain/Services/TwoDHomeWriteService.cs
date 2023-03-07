using SmartHomeManager.Domain.TwoDHomeDomain.Interfaces;

namespace SmartHomeManager.Domain.TwoDHomeDomain.Services;

public class TwoDHomeWriteService
{
    private readonly ITwoDHomeRepository _twoDHomeRepository;

    public TwoDHomeWriteService(ITwoDHomeRepository twoDHomeRepository)
    {
        _twoDHomeRepository = twoDHomeRepository;
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

    public async Task<bool> UpdateRoomCoordinate(Guid roomCoordinateId, int xCoordinate, int yCoordinate, int height,
        int width)
    {
        //If there is no roomCoordinate with given id, return false
        var res = await _twoDHomeRepository.GetRoomCoordinate(roomCoordinateId);
        if (res == null) return false;

        //Get all roomCoordinates
        var roomCoordinateList = await _twoDHomeRepository.GetAllRoomCoordinates();

        //Loop through all roomCoordinates
        foreach (var roomCoordinate in roomCoordinateList)
            // Check for collision between given object and other object
            if (xCoordinate < roomCoordinate.XCoordinate + roomCoordinate.Width &&
                xCoordinate + width > roomCoordinate.XCoordinate &&
                yCoordinate < roomCoordinate.YCoordinate + roomCoordinate.Height &&
                yCoordinate + height > roomCoordinate.YCoordinate)
                // Collision detected, exit loop
                return false;

        // No collision detected, update coordinates and width of given object
        res.XCoordinate = xCoordinate;
        res.YCoordinate = yCoordinate;
        res.Height = height;
        res.Width = width;
        _twoDHomeRepository.UpdateRoomCoordinate(res);
        await _twoDHomeRepository.SaveChangesAsync();

        return true;
    }

    public async Task RemoveRoomCoordinate(Guid id)
    {
        var res = await _twoDHomeRepository.GetRoomCoordinate(id);
        if (res == null) return;
        _twoDHomeRepository.RemoveRoomCoordinate(res);
        await _twoDHomeRepository.SaveChangesAsync();
    }
}