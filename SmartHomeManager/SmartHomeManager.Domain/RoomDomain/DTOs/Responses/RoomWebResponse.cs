namespace SmartHomeManager.Domain.RoomDomain.DTOs.Responses;

public class RoomWebResponse : IRoomWebResponse
{
    public Guid RoomId { get; set; }
    public string Name { get; set; }
    public Guid AccountId { get; set; }
}