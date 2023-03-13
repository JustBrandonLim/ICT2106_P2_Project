namespace SmartHomeManager.Domain.TwoDHomeDomain.Mocks;

public interface IControlDeviceServiceMock
{
    bool SwitchOnDevice(Guid deviceId);
    bool SwitchOffDevice(Guid deviceId);
}