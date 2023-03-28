namespace SmartHomeManager.Domain.TwoDHomeDomain.Entities;

public interface IRoomCoordinate
{
    public Guid RoomCoordinateId { get; set; }
    public int XCoordinate { get; set; }
    public int YCoordinate { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public Guid RoomId { get; set; }
    public bool IsCollidedWith(IRoomCoordinate roomCoordinate);
}