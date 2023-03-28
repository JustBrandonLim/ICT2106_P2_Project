using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.AccountDomain.Interfaces
{
    public interface IEmailRegistrationService
    {
        public bool SendRegistrationEmail(string username, string recipient);
    }
}
