namespace SmartHomeManager.Domain.TwoDHomeDomain.Entities;

public interface IRoomGrid
{
    Guid RoomCoordinateId { get; set; }
    Guid RoomId { get; set; }
    string RoomName { get; set; }
    int XCoordinate { get; set; }
    int YCoordinate { get; set; }
    int Width { get; set; }
    int Height { get; set; }
    List<DeviceControl> DeviceControls { get; set; }
}