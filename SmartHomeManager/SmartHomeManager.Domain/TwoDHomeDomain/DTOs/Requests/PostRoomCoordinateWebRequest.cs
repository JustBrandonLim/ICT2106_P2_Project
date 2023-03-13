namespace SmartHomeManager.Domain.TwoDHomeDomain.DTOs.Requests;

public class PostRoomCoordinateWebRequest
{
    public int XCoordinate { get; set; }
    public int YCoordinate { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public Guid RoomId { get; set; }
}