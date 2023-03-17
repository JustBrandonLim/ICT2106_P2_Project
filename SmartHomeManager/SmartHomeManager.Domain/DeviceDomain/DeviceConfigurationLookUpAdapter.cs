using Newtonsoft.Json.Linq;
using SmartHomeManager.Domain.DeviceDomain.Entities;

namespace SmartHomeManager.Domain.DeviceDomain
{
	public class DeviceConfigurationLookUpAdapter
	{
		private IEnumerable<DeviceConfigurationLookUp> _deviceConfigurationLookUps;

		public DeviceConfigurationLookUpAdapter(IEnumerable<DeviceConfigurationLookUp> deviceConfigurationLookUps)
		{
			_deviceConfigurationLookUps = deviceConfigurationLookUps;
		}

		public JArray ConvertToJson()
		{
			JArray configurations = new();

			foreach (DeviceConfigurationLookUp deviceConfigurationLookUp in _deviceConfigurationLookUps)
			{
				string[] values = deviceConfigurationLookUp.ValueMeaning.Split(",");

                JObject configuration = new()
                {
                    { "name", deviceConfigurationLookUp.ConfigurationKey },
					{ "values", new JArray(values) },
                };

				configurations.Add(configuration);
			}

			return configurations;
		}
	}
}

