using System.ComponentModel.DataAnnotations;

namespace SmartHomeManager.Domain.RoomDomain.DTOs.Requests;

public class PutRoomWebRequest
{
    [Required] public string Name { get; set; }
}