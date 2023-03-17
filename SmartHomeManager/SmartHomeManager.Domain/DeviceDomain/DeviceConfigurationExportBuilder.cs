using System;
using Newtonsoft.Json.Linq;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.DeviceDomain.Interfaces;

namespace SmartHomeManager.Domain.DeviceDomain
{
	public class DeviceConfigurationExportBuilder : IDeviceConfigurationExportBuilder
	{
        private JObject _deviceConfigurationExport;

        public DeviceConfigurationExportBuilder() 
	    {
            _deviceConfigurationExport = new(); 
	    }

        public void BuildDeviceMetadata(Device device)
        {
            JObject deviceMetadataJson = new();
            deviceMetadataJson.Add("deviceName", device.DeviceName);
            deviceMetadataJson.Add("deviceBrand", device.DeviceBrand);
            deviceMetadataJson.Add("deviceModel", device.DeviceModel);
            deviceMetadataJson.Add("deviceTypeName", device.DeviceTypeName);

            _deviceConfigurationExport.Add("deviceMetadata", deviceMetadataJson);
        }

        public void BuildDeviceConfigurations(JArray deviceConfigurationsJson)
        {
            _deviceConfigurationExport.Add("deviceConfigurations", deviceConfigurationsJson);
        }

        public JObject Build() => _deviceConfigurationExport;
    }
}

