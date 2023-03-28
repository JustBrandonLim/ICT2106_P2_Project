namespace SmartHomeManager.Domain.TwoDHomeDomain.Mocks;

public class ControlDeviceServiceMock : IControlDeviceServiceMock
{
    public bool SwitchOnDevice(Guid deviceId)
    {
        return true;
    }

    public bool SwitchOffDevice(Guid deviceId)
    {
        return true;
    }
}