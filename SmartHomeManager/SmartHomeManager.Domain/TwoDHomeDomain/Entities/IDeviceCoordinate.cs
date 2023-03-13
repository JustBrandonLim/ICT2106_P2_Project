namespace SmartHomeManager.Domain.TwoDHomeDomain.Entities;

public interface IDeviceCoordinate
{
    Guid DeviceCoordinateId { get; set; }
    int XCoordinate { get; set; }
    int YCoordinate { get; set; }
    int Width { get; set; }
    int Height { get; set; }
    Guid DeviceId { get; set; }
}