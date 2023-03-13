using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.AccountDomain.Interfaces
{
    public interface IEmailService
    {
        public bool SendRegistrationEmail(string username, string recipient);
        public Task<bool> SendPurchaseEmailConfirmation(Guid accountId);

    }
}
