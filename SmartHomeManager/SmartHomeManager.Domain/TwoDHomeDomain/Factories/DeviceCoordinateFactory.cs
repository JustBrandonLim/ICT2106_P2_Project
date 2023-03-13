using SmartHomeManager.Domain.TwoDHomeDomain.Entities;

namespace SmartHomeManager.Domain.TwoDHomeDomain.Factories;

public class DeviceCoordinateFactory
{
    public static IDeviceCoordinate CreateDeviceCoordinate(int x, int y, int w, int h, Guid deviceId)
    {
        return new DeviceCoordinate
        {
            DeviceCoordinateId = new Guid(),
            XCoordinate = x,
            YCoordinate = y,
            Width = w,
            Height = h,
            DeviceId = deviceId
        };
    }
}