using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.AccountDomain.Interfaces
{
    public interface IProfileDeviceReadService
    {
        public Task<IEnumerable<Guid>?> GetDevicesByProfileId(Guid id);
    }
}
