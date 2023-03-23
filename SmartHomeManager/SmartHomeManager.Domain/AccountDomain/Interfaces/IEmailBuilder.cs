using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.AccountDomain.Product;

namespace SmartHomeManager.Domain.AccountDomain.Interfaces
{
    public interface IEmailBuilder
    {
        public EmailProduct GetProduct();
        public void BuildEmailProduct(string givenBody, string givenRecipient, string givenSubject);
    }
}
