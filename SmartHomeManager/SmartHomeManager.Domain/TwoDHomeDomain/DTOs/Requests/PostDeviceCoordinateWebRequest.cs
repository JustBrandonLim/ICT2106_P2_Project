namespace SmartHomeManager.Domain.TwoDHomeDomain.DTOs.Requests;

public class PostDeviceCoordinateWebRequest
{
    public int XCoordinate { get; set; }
    public int YCoordinate { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public Guid DeviceId { get; set; }
}