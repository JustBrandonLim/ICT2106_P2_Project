﻿using System;
using SmartHomeManager.Domain.DeviceDomain.Entities;

namespace SmartHomeManager.Domain.DeviceDomain.Interfaces
{
	public interface IDeviceRepository
	{
		public Task AddAsync(Device device);

		public Task<Device?> GetAsync(Guid deviceId);

        public void Update(Device device);

        public Task<IEnumerable<Device>> GetAllAsync();

		public Task<bool> SaveAsync();
	}
}

