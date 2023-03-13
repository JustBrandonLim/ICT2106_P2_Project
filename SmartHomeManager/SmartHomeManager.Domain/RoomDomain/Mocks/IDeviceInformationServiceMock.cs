using SmartHomeManager.Domain.DeviceDomain.Entities;

namespace SmartHomeManager.Domain.RoomDomain.Mocks;

public interface IDeviceInformationServiceMock
{
    Device? GetDeviceById(Guid deviceId);
    IEnumerable<Device> GetDevicesInRoom(Guid roomId);
    bool isDeviceOn(Guid deviceId);
}