using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.AccountDomain.DTOs;

namespace SmartHomeManager.Domain.AccountDomain.Interfaces
{
    public interface ITwoFactorAuthService
    {
        public bool ValidateTwoFactorPIN(ValidatePinWebRequest validatePin);
        public QrResponse GenerateTwoFactorAuthentication(Guid accountId);
    }
}
