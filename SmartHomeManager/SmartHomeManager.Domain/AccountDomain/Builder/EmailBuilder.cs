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
using System.Reflection.Metadata.Ecma335;
using SmartHomeManager.Domain.AccountDomain.Product;

namespace SmartHomeManager.Domain.AccountDomain.Builder
{
    public class EmailBuilder : IEmailBuilder
    {
        private const string From = "1004companyemail@gmail.com";
        private const string GoogleAppPassword = "alirejlqrkfqisji";

        private string GivenBody = "";
        private string GivenRecipient = "";
        private string GivenSubject = "";

        private EmailProduct ReusableEmailProduct = new EmailProduct();

        // Reset function for builder
        public void Reset()
        {
            ReusableEmailProduct = new EmailProduct();
        }

        public EmailProduct GetProduct()
        {
            EmailProduct resultProduct = ReusableEmailProduct;
            Reset();

            return resultProduct;
        }

        public void BuildEmailProduct(string givenBody, string givenRecipient, string givenSubject)
        {
            Reset();

            GivenBody = givenBody;
            GivenRecipient = givenRecipient;
            GivenSubject = givenSubject;

            BuildSmtpClient();
            BuildMailMessage();
        }

        public void BuildSmtpClient()
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(From, GoogleAppPassword),
                EnableSsl = true,
            };

            ReusableEmailProduct.Client = smtpClient;
        }

        public void BuildMailMessage()
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(From),
                Subject = GivenSubject,
                Body = GivenBody,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(new MailAddress(GivenRecipient));

            ReusableEmailProduct.Message = mailMessage;
        }


    }
}

