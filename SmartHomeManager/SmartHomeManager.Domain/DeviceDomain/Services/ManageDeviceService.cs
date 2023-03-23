using System;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.DeviceDomain.Interfaces;

namespace SmartHomeManager.Domain.DeviceDomain.Services
{
	public class ManageDeviceService
	{
		private readonly IDeviceRepository _deviceRepository;
		private readonly IDeviceConfigurationLookUpRepository _deviceConfigurationLookUpRepository;
		private readonly IDeviceConfigurationRepository _deviceConfigurationRepository;
		private readonly IDeviceTypeRepository _deviceTypeRepository;

		public ManageDeviceService(IDeviceRepository deviceRepository, IDeviceConfigurationLookUpRepository deviceConfigurationLookUpRepository, IDeviceConfigurationRepository deviceConfigurationRepository, IDeviceTypeRepository deviceTypeRepository)
		{
			_deviceRepository = deviceRepository;
			_deviceConfigurationLookUpRepository = deviceConfigurationLookUpRepository;
			_deviceConfigurationRepository = deviceConfigurationRepository;
			_deviceTypeRepository = deviceTypeRepository;
		}

		public async Task<IEnumerable<Device>> GetAllDevicesByAccountAsync(Guid accountId) 
		{
			IEnumerable<Device> devices = (await _deviceRepository.GetAllAsync()).Where(device => device.AccountId == accountId);

			return devices;
		}

		public async Task<Device?> GetDeviceByIdAsync(Guid deviceId)
		{
			return await _deviceRepository.GetAsync(deviceId);
		}

		public async Task<string> GetDevicePossibleConfigurationsAsync(string deviceBrand, string deviceModel)
		{
			IEnumerable<DeviceConfigurationLookUp> deviceConfigurationLookUps = (await _deviceConfigurationLookUpRepository.GetAllAsync()).Where(deviceConfigurationLookUp => deviceConfigurationLookUp.DeviceBrand == deviceBrand && deviceConfigurationLookUp.DeviceModel == deviceModel);

			JArray jsonArray  = new DeviceConfigurationLookUpAdapter(deviceConfigurationLookUps).ConvertToJson();

            return JsonConvert.SerializeObject(jsonArray, Formatting.Indented);
		}

		public async Task<string> GetDeviceConfigurationsAsync(Guid deviceId, string deviceBrand, string deviceModel)
		{
			IEnumerable<DeviceConfiguration> deviceConfigurations = (await _deviceConfigurationRepository.GetAllAsync()).Where(deviceConfiguration => deviceConfiguration.DeviceId == deviceId && deviceConfiguration.DeviceBrand == deviceBrand && deviceConfiguration.DeviceModel == deviceModel);

			JArray jsonArray  = new DeviceConfigurationAdapter(deviceConfigurations).ConvertToJson();

			return JsonConvert.SerializeObject(jsonArray, Formatting.Indented);
		}

		public async Task<bool> ApplyDeviceConfigurationAsync(string configurationKey, string deviceBrand, string deviceModel, Guid deviceId, int configurationValue)
		{ 
			try
            {
				DeviceConfiguration deviceConfiguration = new()
				{
					ConfigurationKey = configurationKey,
					DeviceBrand = deviceBrand,
					DeviceModel = deviceModel,
					DeviceId = deviceId,
					ConfigurationValue = configurationValue,
                };

				DeviceConfiguration? existingDeviceConfiguration = await _deviceConfigurationRepository.GetAsync(configurationKey, deviceId);
				if (existingDeviceConfiguration != null)
				{
					existingDeviceConfiguration.ConfigurationValue = configurationValue;

					_deviceConfigurationRepository.Update(existingDeviceConfiguration);
				}
				else
				{
					await _deviceConfigurationRepository.AddAsync(deviceConfiguration);
				}

                return await _deviceConfigurationRepository.SaveAsync();
            }
            catch (Exception e)
	        {
				Debug.WriteLine(e.Message);
                return false;
            }
		}

		public async Task<bool> ApplyDeviceConfigurationsSameTypeAsync(string configurationKey, string deviceBrand, string deviceModel, Guid deviceId, int configurationValue)
		{ 
			try
            {
				IEnumerable<DeviceConfigurationLookUp> deviceConfigurationLookUps = (await _deviceConfigurationLookUpRepository.GetAllAsync()).Where(deviceConfigurationLookUp => deviceConfigurationLookUp.DeviceBrand == deviceBrand && deviceConfigurationLookUp.DeviceModel == deviceModel);
				IEnumerable<Device> devices = (IEnumerable<Device>)(await _deviceRepository.GetAllAsync());

				foreach (Device device in devices) 
				{
					if (device.DeviceBrand == deviceBrand && device.DeviceModel == deviceModel) 
					{
						DeviceConfiguration deviceConfiguration = new()
						{
							ConfigurationKey = configurationKey,
							DeviceBrand = deviceBrand,
							DeviceModel = deviceModel,
							DeviceId = deviceId,
							ConfigurationValue = configurationValue,
						};

						DeviceConfiguration? existingDeviceConfiguration = await _deviceConfigurationRepository.GetAsync(configurationKey, device.DeviceId);
						if (existingDeviceConfiguration != null)
						{
							existingDeviceConfiguration.ConfigurationValue = configurationValue;

							_deviceConfigurationRepository.Update(existingDeviceConfiguration);
						}
						else
						{
							await _deviceConfigurationRepository.AddAsync(deviceConfiguration);
						}
					}
				}

				return await _deviceConfigurationRepository.SaveAsync();
            }
            catch (Exception e)
	        {
				Debug.WriteLine(e.Message);
                return false;
            }
		}

		public async Task<bool> ApplyDeviceMetadataAsync(Guid deviceId, string deviceName, string devicePassword, string deviceTypeName)
		{
            try
            {
                Device device = new()
                {
                    DeviceId = deviceId,
                    DeviceName = deviceName,
					DevicePassword = devicePassword,
					DeviceTypeName = deviceTypeName,
                };

                Device? existingDevice = await _deviceRepository.GetAsync(deviceId);
                if (existingDevice != null)
                {
					existingDevice.DeviceName = deviceName;
					existingDevice.DevicePassword = devicePassword;
					existingDevice.DeviceTypeName = deviceTypeName;

                    _deviceRepository.Update(existingDevice);
                }

                return await _deviceRepository.SaveAsync();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

		public async Task<bool> SetDevicePasswordByIdAsync(Guid deviceId, string devicePassword)
		{
            try
            {
                Device device = new()
                {
                    DeviceId = deviceId,
					DevicePassword = devicePassword,
                };

                Device? existingDevice = await _deviceRepository.GetAsync(deviceId);
                if (existingDevice != null)
                {
					existingDevice.DevicePassword = devicePassword;

                    _deviceRepository.Update(existingDevice);
                }

                return await _deviceRepository.SaveAsync();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

		public async Task<string> ExportDeviceConfigurationsAsync(Guid deviceId, string deviceBrand, string deviceModel) 
		{
			IDeviceConfigurationExportBuilder deviceConfigurationExportBuilder = new DeviceConfigurationExportBuilder();

			Device deviceFromDb = (await _deviceRepository.GetAsync(deviceId))!;

			deviceConfigurationExportBuilder.BuildDeviceMetadata(deviceFromDb);

			IEnumerable<DeviceConfiguration> deviceConfigurations = (await _deviceConfigurationRepository.GetAllAsync()).Where(deviceConfiguration => deviceConfiguration.DeviceId == deviceId && deviceConfiguration.DeviceBrand == deviceBrand && deviceConfiguration.DeviceModel == deviceModel);
			JArray deviceConfigurationsJson = new DeviceConfigurationAdapter(deviceConfigurations).ConvertToJson();

			deviceConfigurationExportBuilder.BuildDeviceConfigurations(deviceConfigurationsJson);

			return JsonConvert.SerializeObject(deviceConfigurationExportBuilder.Build(), Formatting.Indented);	
		}

		public async Task<bool> ImportDeviceConfigurationsAsync(Guid deviceId, string deviceConfigurationJson) 
		{
			try
			{
				//JArray jsonArray  = new DeviceConfigurationAdapter(deviceConfigurations).ConvertToJson();
				JObject jObject = JObject.Parse(deviceConfigurationJson);

                Device? existingDevice = await _deviceRepository.GetAsync(deviceId);
				if (existingDevice != null)
				{
					existingDevice.DeviceName = (string)(jObject["deviceMetadata"]!["deviceName"])!;
					existingDevice.DeviceBrand = (string)(jObject["deviceMetadata"]!["deviceBrand"])!;
					existingDevice.DeviceModel = (string)(jObject["deviceMetadata"]!["deviceModel"])!;

					existingDevice.DeviceName = (string)(jObject["deviceMetadata"]!["deviceName"])!;

					bool deviceTypeNameExist = (await _deviceTypeRepository.GetAllAsync()).Where(deviceType => deviceType.DeviceTypeName == (string)(jObject["deviceMetadata"]!["deviceTypeName"])!).Count() == 1;

					if (!deviceTypeNameExist)
					{
						DeviceType deviceType = new()
						{
							DeviceTypeName = (string)(jObject["deviceMetadata"]!["deviceTypeName"])!
						};

						await _deviceTypeRepository.AddAsync(deviceType);

						await _deviceTypeRepository.SaveAsync();
					}

					existingDevice.DeviceTypeName = (string)(jObject["deviceMetadata"]!["deviceTypeName"])!;

					_deviceRepository.Update(existingDevice);
					await _deviceRepository.SaveAsync();

					JArray deviceConfigurations = (JArray)jObject["deviceConfigurations"]!;

					foreach (JObject deviceConfiguration in deviceConfigurations)
					{
						DeviceConfiguration newDeviceConfiguration = new()
						{
							ConfigurationKey = (string)deviceConfiguration["configurationKey"]!,
							DeviceBrand = existingDevice.DeviceBrand,
							DeviceModel = existingDevice.DeviceModel,
							DeviceId = deviceId,
							ConfigurationValue = (int)deviceConfiguration["configurationValue"]!,
						};

						DeviceConfiguration? existingDeviceConfiguration = await _deviceConfigurationRepository.GetAsync(newDeviceConfiguration.ConfigurationKey, deviceId);
						if (existingDeviceConfiguration != null)
						{
							existingDeviceConfiguration.ConfigurationValue = newDeviceConfiguration.ConfigurationValue;

							_deviceConfigurationRepository.Update(existingDeviceConfiguration);
						}
						else
						{
							await _deviceConfigurationRepository.AddAsync(newDeviceConfiguration);
						}

						await _deviceRepository.SaveAsync();
					}
				}

				return true;
			}
			catch (Exception e)
			{
				return false;
			}
		}
	}
}

