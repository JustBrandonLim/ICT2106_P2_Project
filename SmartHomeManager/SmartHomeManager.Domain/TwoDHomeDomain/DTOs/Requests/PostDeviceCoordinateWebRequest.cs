using System.ComponentModel.DataAnnotations;

namespace SmartHomeManager.Domain.TwoDHomeDomain.DTOs.Requests;

// Using concrete class because ASP.Net does not support serialization of interface types,
// thus a concrete class is used instead. This is because the request objects used in the controllers
// methods must use function parameters that are concrete types for ASP.net for serialization.
// In addition, this is unlikely to change unless the client requirements change,
// thus we decided to use the concrete class instead
public class PostDeviceCoordinateWebRequest
{
    [Required] public int XCoordinate { get; set; }
    [Required] public int YCoordinate { get; set; }
    [Required] public int Width { get; set; }
    [Required] public int Height { get; set; }
    [Required] public Guid DeviceId { get; set; }
}