namespace SmartHomeManager.Domain.TwoDHomeDomain.Entities;

// using concrete classes for DeviceControl because ASP.Net does not support serialization of interface types,
// thus a concrete class is used instead.
// in addition, this is unlikely to change unless the client requirements change,
// thus we decided to use the concrete class instead
public class DeviceControl
{
    public Guid DeviceId { get; set; }
    public string DeviceName { get; set; }
    public bool IsOn { get; set; }
}