using SmartHomeManager.Domain.DeviceDomain.Entities;

namespace SmartHomeManager.Domain.RoomDomain.Mocks;

public interface IDeviceInformationServiceMock
{
    IEnumerable<Device> GetDevicesInRoom(Guid roomId);
    bool isDeviceOn(Guid deviceId);
}