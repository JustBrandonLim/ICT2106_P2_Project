using System;
using System.ComponentModel.DataAnnotations;

namespace SmartHomeManager.Domain.DeviceDomain.Entities.DTOs
{
	public class DeviceMetadataWebRequest
	{
        [Required]
        public Guid DeviceId { get; set; }

        [Required]
        public string DeviceName { get; set; }

        [Required]
        public string DeviceTypeName { get; set; }

        public string DevicePassword { get; set; }
    }
}

