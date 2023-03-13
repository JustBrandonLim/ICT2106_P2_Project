namespace SmartHomeManager.Domain.TwoDHomeDomain.Entities;

public interface IRoomCoordinate
{
    Guid RoomCoordinateId { get; set; }
    int XCoordinate { get; set; }
    int YCoordinate { get; set; }
    int Width { get; set; }
    int Height { get; set; }
    Guid RoomId { get; set; }
    bool IsCollidedWith(IRoomCoordinate roomCoordinate);
}