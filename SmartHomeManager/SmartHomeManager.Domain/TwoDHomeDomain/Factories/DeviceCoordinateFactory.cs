using SmartHomeManager.Domain.TwoDHomeDomain.Entities;

namespace SmartHomeManager.Domain.TwoDHomeDomain.Factories;

public class DeviceCoordinateFactory
{
    public static IDeviceCoordinate CreateDeviceCoordinate(Guid deviceCoordinateId, int xCoordinate, int yCoordinate,
        int width, int height, Guid deviceId)
    {
        return new DeviceCoordinate
        {
            DeviceCoordinateId = deviceCoordinateId,
            XCoordinate = xCoordinate,
            YCoordinate = yCoordinate,
            Width = width,
            Height = height,
            DeviceId = deviceId
        };
    }
}