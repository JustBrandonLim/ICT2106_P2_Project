using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Diagnostics;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.AccountDomain.Interfaces;
using SmartHomeManager.Domain.AccountDomain.DTOs;
using SmartHomeManager.Domain.AccountDomain.Product;

namespace SmartHomeManager.Domain.AccountDomain.Services
{
    public class EmailService : IEmailPurchaseService, IEmailRegistrationService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEmailBuilder _emailBuilder;

        public EmailService(IAccountRepository accountRepository, IEmailBuilder emailBuilder)
        {
            _accountRepository = accountRepository;
            _emailBuilder = emailBuilder;
        }

        public bool SendRegistrationEmail(string username, string recipient)
        {
            string messageBody = $"<div><h2>Hello {username},</h2>  <h2>Thank you for registering an account with Smart Home Manager, we hope you enjoy your experience.</h2></div>";
            string subject = "Company Account Registration";

            _emailBuilder.BuildEmailProduct(messageBody, recipient, subject);
            EmailProduct emailProduct = _emailBuilder.GetProduct();

            try
            {
                emailProduct.Client.Send(emailProduct.Message);
                return true;
            }

            catch (Exception e)
            {
                Debug.WriteLine(e);
                return false;
            }
        }
        public async Task<bool> SendPurchaseEmailConfirmation(Guid accountId)
        {

            Account? account = await _accountRepository.GetByIdAsync(accountId);

            string messageBody = "You have purchased device xxx";

            if (account == null)
            {
                return false;

            }
            else
            {
                try
                {
                    _emailBuilder.BuildEmailProduct(messageBody, account.Email.ToString(), "Purchase Confirmation");
                    EmailProduct emailProduct = _emailBuilder.GetProduct();

                    emailProduct.Client.Send(emailProduct.Message);
                    return true;
                }

                catch (Exception e)
                {
                    Debug.WriteLine(e);
                    return false;
                }
            }
        }
    }
}

