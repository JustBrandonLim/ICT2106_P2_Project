using System;
using Newtonsoft.Json.Linq;
using SmartHomeManager.Domain.DeviceDomain.Entities;

namespace SmartHomeManager.Domain.DeviceDomain.Interfaces
{
	public interface IDeviceConfigurationExportBuilder
	{
		void BuildDeviceMetadata(Device deviceMetadata);

		void BuildDeviceConfigurations(JArray deviceConfigurationsJson);

		JObject Build();
	}
}

