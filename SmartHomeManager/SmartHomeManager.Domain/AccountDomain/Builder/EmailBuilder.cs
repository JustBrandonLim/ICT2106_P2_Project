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

        private EmailProduct emailProduct = new EmailProduct();

        // Reset function for builder
        public void Reset()
        {
            emailProduct = new EmailProduct();
        }

        public EmailProduct GetProduct()
        {
            EmailProduct resultProduct = emailProduct;
            Reset();

            return resultProduct;
        }

        public void BuildEmailProduct(string givenBody, string givenRecipient, string givenSubject)
        {
            Reset();

            this.givenBody = givenBody;
            this.givenRecipient = givenRecipient;
            this.givenSubject = givenSubject;

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

            emailProduct.Client = smtpClient;
        }

        public void BuildMailMessage()
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(From),
                Subject = givenSubject,
                Body = givenBody,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(new MailAddress(givenRecipient));

            emailProduct.Message = mailMessage;
        }


    }
}

