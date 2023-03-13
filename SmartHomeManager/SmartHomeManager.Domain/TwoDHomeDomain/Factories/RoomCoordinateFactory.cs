using SmartHomeManager.Domain.TwoDHomeDomain.Entities;

namespace SmartHomeManager.Domain.TwoDHomeDomain.Factories;

public class RoomCoordinateFactory
{
    public static IRoomCoordinate CreateRoomCoordinate(int x, int y, int w, int h, Guid roomId)
    {
        return new RoomCoordinate
        {
            RoomCoordinateId = new Guid(),
            XCoordinate = x,
            YCoordinate = y,
            Width = w,
            Height = h,
            RoomId = roomId
        };
    }
}