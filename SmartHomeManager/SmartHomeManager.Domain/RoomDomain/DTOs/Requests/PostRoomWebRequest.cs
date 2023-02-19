using System.ComponentModel.DataAnnotations;

namespace SmartHomeManager.Domain.RoomDomain.DTOs.Requests;

public class PostRoomWebRequest
{
    [Required] public string Name { get; set; }

    [Required] public Guid AccountId { get; set; }
}