namespace SmartHomeManager.Domain.TwoDHomeDomain.Entities;

public class RoomGrid
{
    public Guid RoomCoordinateId { get; set; }
    public Guid RoomId { get; set; }
    public string RoomName { get; set; }
    public int XCoordinate { get; set; }
    public int YCoordinate { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public List<DeviceControl> DeviceControls { get; set; }
}