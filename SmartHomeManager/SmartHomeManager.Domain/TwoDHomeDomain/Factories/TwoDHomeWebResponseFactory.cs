using SmartHomeManager.Domain.TwoDHomeDomain.DTOs.Responses;
using SmartHomeManager.Domain.TwoDHomeDomain.Entities;

namespace SmartHomeManager.Domain.TwoDHomeDomain.Factories;

public class TwoDHomeWebResponseFactory
{
    public static ITwoDHomeWebResponse CreateRoomWebResponse(List<RoomGrid> roomGrids)
    {
        return new TwoDHomeWebResponse
        {
            RoomGrids = roomGrids
        };
    }
}