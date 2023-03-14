using SmartHomeManager.Domain.TwoDHomeDomain.DTOs.Responses;
using SmartHomeManager.Domain.TwoDHomeDomain.Entities;

namespace SmartHomeManager.Domain.TwoDHomeDomain.Factories;

public class DeviceCoordinateResponseFactory
{
    public static IDeviceCoordinateResponse CreateDeviceCoordinateResponse(IDeviceCoordinate deviceCoordinate)
    {
        return new DeviceCoordinateResponse
        {
            DeviceCoordinateId = deviceCoordinate.DeviceCoordinateId,
            XCoordinate = deviceCoordinate.XCoordinate,
            YCoordinate = deviceCoordinate.YCoordinate,
            Width = deviceCoordinate.Width,
            Height = deviceCoordinate.Height,
            DeviceId = deviceCoordinate.DeviceId
        };
    }
}