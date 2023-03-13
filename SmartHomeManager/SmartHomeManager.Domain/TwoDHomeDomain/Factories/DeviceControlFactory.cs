using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.RoomDomain.Entities;
using SmartHomeManager.Domain.TwoDHomeDomain.Entities;

namespace SmartHomeManager.Domain.TwoDHomeDomain.Factories;

public class DeviceControlFactory
{
    public static List<DeviceControl> CreateDeviceControl(List<Device> devices, Dictionary<Guid, bool> deviceStates)
    {
        var list = new List<DeviceControl>();
        foreach (var device in devices)
            list.Add(new DeviceControl
            {
                DeviceId = device.DeviceId,
                DeviceName = device.DeviceName,
                IsOn = deviceStates[device.DeviceId]
            });
        return list;
    }
}