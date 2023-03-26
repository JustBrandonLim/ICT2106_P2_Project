using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.DeviceDomain.Entities;

namespace SmartHomeManager.Domain.DeviceLoggingDomain.Mocks
{
    public interface IProfileMockService
    {
        IEnumerable<Device> GetDevicesByProfile(Guid profileId);
    }
}
