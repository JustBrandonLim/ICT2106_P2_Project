using SmartHomeManager.Domain.TwoDHomeDomain.DTOs.Responses;
using SmartHomeManager.Domain.TwoDHomeDomain.Entities;

namespace SmartHomeManager.Domain.TwoDHomeDomain.Factories;

public class RoomCoordinateResponseFactory
{
    public static IRoomCoordinateResponse CreateRoomCoordinateResponse(IRoomCoordinate roomCoordinate)
    {
        return new RoomCoordinateResponse
        {
            RoomCoordinateId = roomCoordinate.RoomCoordinateId,
            XCoordinate = roomCoordinate.XCoordinate,
            YCoordinate = roomCoordinate.YCoordinate,
            Width = roomCoordinate.Width,
            Height = roomCoordinate.Height,
            RoomId = roomCoordinate.RoomId
        };
    }
}