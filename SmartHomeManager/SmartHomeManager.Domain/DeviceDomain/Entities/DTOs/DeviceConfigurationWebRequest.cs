using System;
using System.ComponentModel.DataAnnotations;

namespace SmartHomeManager.Domain.DeviceDomain.Entities.DTOs
{
	public class DeviceConfigurationWebRequest
	{
	    [Key]
        public string ConfigurationKey { get; set; }

        [Key]
        public string DeviceBrand { get; set; }

        [Key]
        public string DeviceModel { get; set; }

        [Key]
        public Guid DeviceId { get; set; }
        
        [Required]
        public int ConfigurationValue { get; set; }
	}
}

