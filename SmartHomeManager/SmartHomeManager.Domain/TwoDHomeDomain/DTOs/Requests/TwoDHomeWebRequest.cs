using System.ComponentModel.DataAnnotations;
using SmartHomeManager.Domain.TwoDHomeDomain.Entities;

namespace SmartHomeManager.Domain.TwoDHomeDomain.DTOs.Requests;

// using concrete classes for RoomGrid because ASP.Net does not support serialization of interface types,
// thus a concrete class is used instead.
// in addition, this is unlikely to change unless the client requirements change,
// thus we decided to use the concrete class instead
public class TwoDHomeWebRequest
{
    [Required] public List<RoomGrid> RoomGrids { get; set; }
}