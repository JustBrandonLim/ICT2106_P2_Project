using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SmartHomeManager.Domain.DeviceDomain.Entities;

namespace SmartHomeManager.Domain.TwoDHomeDomain.Entities;

public class DeviceCoordinate
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid DeviceCoordinateId { get; set; }

    [Required] public int XCoordinate { get; set; }

    [Required] public int YCoordinate { get; set; }

    [Required] public int Width { get; set; }

    [Required] public int Height { get; set; }

    [Required] public Guid DeviceId { get; set; }

    [ForeignKey("DeviceId")] public Device Device { get; set; }
}