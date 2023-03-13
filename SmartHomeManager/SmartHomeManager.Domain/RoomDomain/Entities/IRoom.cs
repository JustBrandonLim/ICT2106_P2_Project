using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.TwoDHomeDomain.Entities;

namespace SmartHomeManager.Domain.RoomDomain.Entities;

public interface IRoom
{
    // This declares the properties that must be implemented by the class that implements this interface
    // it is not creating a field inside an interface
    public Guid RoomId { get; }
    public string Name { get; set; }
    public Guid AccountId { get; }
    public RoomCoordinate? RoomCoordinate { get; set; }
    public List<Device> Devices { get; set; }
}