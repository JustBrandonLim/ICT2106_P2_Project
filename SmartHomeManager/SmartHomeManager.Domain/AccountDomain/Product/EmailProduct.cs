using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.AccountDomain.Product
{
    public class EmailProduct
    {
        public SmtpClient Client { get; set; }
        public MailMessage Message { get; set; }
    }
}
