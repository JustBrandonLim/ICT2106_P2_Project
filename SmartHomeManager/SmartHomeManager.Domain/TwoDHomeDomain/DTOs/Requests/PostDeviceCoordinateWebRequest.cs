using System.ComponentModel.DataAnnotations;

namespace SmartHomeManager.Domain.TwoDHomeDomain.DTOs.Requests;

public class PostDeviceCoordinateWebRequest
{
    [Required] public int XCoordinate { get; set; }
    [Required] public int YCoordinate { get; set; }
    [Required] public int Width { get; set; }
    [Required] public int Height { get; set; }
    [Required] public Guid DeviceId { get; set; }
}