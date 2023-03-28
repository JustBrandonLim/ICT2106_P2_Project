﻿using System;
namespace SmartHomeManager.Domain.DeviceDomain.Interfaces.Provides
{
	public interface IDirectorControlDeviceService
	{
        public Task<bool> SetDeviceTypeConfiguration(Guid accountId, string deviceTypeName, string configurationKey, int configurationValue);
    }
}

