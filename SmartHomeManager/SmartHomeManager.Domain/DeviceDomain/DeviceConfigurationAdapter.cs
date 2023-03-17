using Newtonsoft.Json.Linq;
using SmartHomeManager.Domain.DeviceDomain.Entities;

namespace SmartHomeManager.Domain.DeviceDomain
{
	public class DeviceConfigurationAdapter
	{
		private IEnumerable<DeviceConfiguration> _deviceConfigurations;

		public DeviceConfigurationAdapter(IEnumerable<DeviceConfiguration> deviceConfigurations)
		{
			_deviceConfigurations = deviceConfigurations;
		}

		public JArray ConvertToJson()
		{
			JArray configurations = new();

			foreach (DeviceConfiguration deviceConfiguration in _deviceConfigurations)
			{
				JObject configuration = new()
				{
					{ "configurationKey", deviceConfiguration.ConfigurationKey},
					{ "configurationValue", deviceConfiguration.ConfigurationValue },
				};

				configurations.Add(configuration);
			}

			return configurations;

		}
	}
}

