using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SmartHomeManager.Domain.RoomDomain.Entities;

namespace SmartHomeManager.Domain.TwoDHomeDomain.Entities;

public class RoomCoordinate
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid RoomCoordinateId { get; set; }

    [Required] public int XCoordinate { get; set; }

    [Required] public int YCoordinate { get; set; }

    [Required] public int Width { get; set; }

    [Required] public int Height { get; set; }

    [Required] public Guid RoomId { get; set; }

    [ForeignKey("RoomId")] public Room Room { get; set; }
}