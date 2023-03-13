namespace SmartHomeManager.Domain.TwoDHomeDomain.Entities;

public class DeviceControl
{
    public Guid DeviceId { get; set; }
    public string DeviceName { get; set; }
    public bool IsOn { get; set; }
}