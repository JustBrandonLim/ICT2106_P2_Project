namespace SmartHomeManager.Domain.RoomDomain.Entities;

public interface IRoom
{
    // This declares the properties that must be implemented by the class that implements this interface
    public Guid RoomId { get; }
    public string Name { get; set; }
    public Guid AccountId { get; }
}