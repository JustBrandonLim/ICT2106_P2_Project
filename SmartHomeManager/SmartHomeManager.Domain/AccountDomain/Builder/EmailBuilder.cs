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

        private string givenBody = "";
        private string givenRecipient = "";
        private string givenSubject = "";

        public EmailProduct BuildEmailProduct(string givenBody, string givenRecipient, string givenSubject)
        {
            this.givenBody = givenBody;
            this.givenRecipient = givenRecipient;
            this.givenSubject = givenSubject;

            EmailProduct newEmailProduct = new()
            {
                Client = BuildSmtpClient(),
                Message = BuildMailMessage()
            };

            return newEmailProduct;
        }

        public SmtpClient BuildSmtpClient()
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(From, GoogleAppPassword),
                EnableSsl = true,
            };

            return smtpClient;
        }

        public MailMessage BuildMailMessage()
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(From),
                Subject = givenSubject,
                Body = givenBody,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(new MailAddress(givenRecipient));

            return mailMessage;
        }


    }
}

