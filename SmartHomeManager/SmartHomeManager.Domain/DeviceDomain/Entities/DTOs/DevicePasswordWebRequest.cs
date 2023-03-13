using System;
using System.ComponentModel.DataAnnotations;

namespace SmartHomeManager.Domain.DeviceDomain.Entities.DTOs
{
    public class DevicePasswordWebRequest
    {
        [Key]
        public Guid DeviceId { get; set; }

        public string DevicePassword { get; set; }
    }
}

