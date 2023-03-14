using System.ComponentModel.DataAnnotations;

namespace SmartHomeManager.Domain.TwoDHomeDomain.DTOs.Requests;

public class PostRoomCoordinateWebRequest
{
    [Required] public int XCoordinate { get; set; }
    [Required] public int YCoordinate { get; set; }
    [Required] public int Width { get; set; }
    [Required] public int Height { get; set; }
    [Required] public Guid RoomId { get; set; }
}