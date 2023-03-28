namespace SmartHomeManager.Domain.RoomDomain.DTOs.Responses;

public interface IRoomWebResponse
{
    public Guid RoomId { get; set; }
    public string Name { get; set; }
    public Guid AccountId { get; set; }
}