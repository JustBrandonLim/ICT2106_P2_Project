using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SmartHomeManager.Domain.RoomDomain.Entities;

namespace SmartHomeManager.Domain.TwoDHomeDomain.Entities;

public class RoomCoordinate : IRoomCoordinate
{
    [ForeignKey("RoomId")] public Room Room { get; set; }

    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid RoomCoordinateId { get; set; }

    [Required] public int XCoordinate { get; set; }

    [Required] public int YCoordinate { get; set; }

    [Required] public int Width { get; set; }

    [Required] public int Height { get; set; }

    [Required] public Guid RoomId { get; set; }

    public bool IsCollidedWith(IRoomCoordinate roomCoordinate)
    {
        // Check for collision between current object and the other object
        if (XCoordinate < roomCoordinate.XCoordinate + roomCoordinate.Width &&
            XCoordinate + roomCoordinate.Width > roomCoordinate.XCoordinate &&
            YCoordinate < roomCoordinate.YCoordinate + roomCoordinate.Height &&
            YCoordinate + roomCoordinate.Height > roomCoordinate.YCoordinate)
            return true;
        return false;
    }
}