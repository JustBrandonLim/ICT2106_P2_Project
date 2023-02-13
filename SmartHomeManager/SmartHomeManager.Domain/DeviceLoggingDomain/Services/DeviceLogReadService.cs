﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.DeviceLoggingDomain.Entities.DTO;
using SmartHomeManager.Domain.DeviceLoggingDomain.Interfaces;
using SmartHomeManager.Domain.DeviceLoggingDomain.Mocks;
using SmartHomeManager.Domain.RoomDomain.Entities;
using static System.Reflection.Metadata.BlobBuilder;

namespace SmartHomeManager.Domain.DeviceLoggingDomain.Services
{
    // get all devices from profile first
    // get device log by date and time
    // get watts from devices

    public class DeviceLogReadService
    {
        private readonly IDeviceLogRepository _deviceLogRepository;
        private readonly IProfileService _profileService;
        private readonly IDeviceWattsService _deviceWattsService;

        public DeviceLogReadService(IDeviceLogRepository deviceLogRepository, IProfileService profileService, IDeviceWattsService deviceWattsService)
        {
            _deviceLogRepository = deviceLogRepository;
            _profileService = profileService;
            _deviceWattsService = deviceWattsService;
        }

        // get devices from profile
        public IEnumerable<Device> GetAllDevicesInProfile(Guid profileId){
            return _profileService.GetAllDevicesInProfile(profileId);
        }


        // get watts from devices
        public int getDeviceWatts(Guid deviceId){
            return _deviceWattsService.getDeviceWatts(deviceId);
        }
        // look for logs (to update)
        public async Task<GetDeviceLogWebRequest> GetDeviceLogByDate(DateTime date, Guid deviceId, bool deviceState)
        {
            var res = await _deviceLogRepository.GetByDate(date.Date, deviceId, deviceState);
            if (res == null) return null;

            var ret = new GetDeviceLogWebRequest
            {
                StartTime = res.StartTime,
                EndTime = res.EndTime,
                DateLogged = res.DateLogged,
                DeviceEnergyUsage = res.DeviceEnergyUsage,
                DeviceActivity = res.DeviceActivity,
                DeviceState = res.DeviceState
            };

            return ret;
            

        }
        // using this i already can get by week and day. 
        public IEnumerable<GetDeviceLogWebRequest> GetDeviceLogByDateAndTime(DateTime date, DateTime startTime, DateTime endTime)
        {
            var res =  _deviceLogRepository.Get(date.Date,startTime, endTime);
            var resp = res.Select(log => new GetDeviceLogWebRequest
            {
                StartTime = log.StartTime,
                EndTime = log.EndTime,
                DateLogged = log.DateLogged,
                DeviceEnergyUsage = log.DeviceEnergyUsage,
                DeviceActivity = log.DeviceActivity,
                DeviceState = log.DeviceState
            }).ToList();

            return resp;

        }

        public async Task<IEnumerable<GetDeviceLogWebRequest>> GetAllDeviceLogs()
        {
            var result = await _deviceLogRepository.GetAll();
            var resp = result.Select(dLogs => new GetDeviceLogWebRequest
            {
                StartTime = dLogs.StartTime,
                EndTime = dLogs.EndTime,
                DateLogged = dLogs.DateLogged,
                DeviceEnergyUsage = dLogs.DeviceEnergyUsage,
                DeviceActivity = dLogs.DeviceActivity,
                DeviceState = dLogs.DeviceState

            }).ToList();
            return resp;
        }
    }
}
