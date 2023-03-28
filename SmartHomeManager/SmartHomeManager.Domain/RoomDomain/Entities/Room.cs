using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.TwoDHomeDomain.Entities;

namespace SmartHomeManager.Domain.RoomDomain.Entities;

public class Room : IRoom
{
    [ForeignKey("AccountId")] public Account Account { get; set; }

    public RoomCoordinate? RoomCoordinate { get; set; }

    public List<Device> Devices { get; set; }

    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid RoomId { get; set; }

    [Required] public string Name { get; set; }
    [Required] public Guid AccountId { get; set; }
}