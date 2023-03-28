using SmartHomeManager.Domain.TwoDHomeDomain.Entities;

namespace SmartHomeManager.Domain.TwoDHomeDomain.Factories;

public class RoomCoordinateFactory
{
    public static IRoomCoordinate CreateRoomCoordinate(Guid roomCoordinateId, int x, int y, int w, int h, Guid roomId)
    {
        return new RoomCoordinate
        {
            RoomCoordinateId = roomCoordinateId,
            XCoordinate = x,
            YCoordinate = y,
            Width = w,
            Height = h,
            RoomId = roomId
        };
    }
}