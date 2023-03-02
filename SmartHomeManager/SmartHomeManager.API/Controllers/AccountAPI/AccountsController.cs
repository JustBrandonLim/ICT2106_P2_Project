using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using SmartHomeManager.DataSource;
using SmartHomeManager.DataSource.AccountDataSource;
using SmartHomeManager.Domain.AccountDomain.DTOs;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.AccountDomain.Interfaces;
using SmartHomeManager.Domain.AccountDomain.Services;


namespace SmartHomeManager.API.Controllers.AccountAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        // dependency injection is just saying this class can use this thing
        // in this context - accounts controller can use account service
        private readonly AccountService _accountService;
        private readonly EmailService _emailService;
        private readonly TwoFactorAuthService _twoFactorAuthService;

        public AccountsController(IAccountRepository accountRepository, IAccountService accountService, 
            IEmailService emailService, ITwoFactorAuthService twoFactorAuthService)
        {
            _accountService = new(accountRepository);
            _emailService = new(accountRepository);
            _twoFactorAuthService = new();
        }

        /* 
         * GET: api/Accounts
         * Return: 
         * Ok(accounts) - IEnumerable of accounts
         * NotFound(1) - No accounts in DB
        */
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
            IEnumerable<Account> accounts = await _accountService.GetAccounts();

            if (!accounts.Any())
            {
                return NotFound(1);
            }

            return Ok(accounts);
        }

        /* 
         * GET: api/Accounts/11111111-1111-1111-1111-111111111111
         * Return: 
         * Ok(account) - Account as requested
         * NotFound(1) - Account does not exist
        */
        [HttpGet("{accountId}")]
        public async Task<ActionResult<Account>> GetAccountByAccountId(Guid accountId)
        {
            Account? account = await _accountService.GetAccountByAccountId(accountId);

            if (account == null)
            {
                return NotFound(1);
            }

            return Ok(account);
        }

        /* 
         * PUT: api/Accounts/11111111-1111-1111-1111-111111111111
         * Return:
         * Ok(1) - Account successfully updated
         * BadRequest(1) - Account failed to update
         * NotFound(1) - Account does not exist
         * 
        */
        [HttpPut("{accountId}")]
        public async Task<IActionResult> PutAccount(Guid accountId, [FromBody] AccountWebRequest accountWebRequest)
        {
            Account? account = await _accountService.GetAccountByAccountId(accountId);
            if (account == null)
            {
                return NotFound(1);
            }

            if (await _accountService.UpdateAccount(account, accountWebRequest))
            {
                return Ok(1);
            }

            return BadRequest(1);
        }

        /* 
         * POST: api/Accounts
         * Return:
         * Ok(1) - Account created & email Sent
         * Ok(2) - Account created but email not sent
         * BadRequest(1) - Account failed to create
         * BadRequest(2) - Email already exists
         * 
        */
        [HttpPost]
        public async Task<ActionResult> PostAccount([FromBody] AccountWebRequest accountWebRequest)
        {

            // controller will invoke a service function
            int response = await _accountService.CreateAccount(accountWebRequest);

            // if create account is successful
            if (response == 1)
            {
                // Email service
                bool emailResponse = _emailService.SendRegistrationEmail(accountWebRequest.Username, accountWebRequest.Email);
                
                if (emailResponse)
                {
                    // if everything is ok
                    return Ok(1);
                }

                // if account created, but email not sent
                return Ok(2);
            }

            // if create account is unsuccessful
            else if (response == 2)
            {
                return BadRequest(1);
            }

            // email already exists
            return BadRequest(2);
            
        }

        /*
         * POST: api/Accounts/login
         * Return:
         * Ok(1) - Login successful
         * BadRequest(1) - Login unsuccessful, wrong password
         * BadRequest(2) - Login unsuccessful, account does not exist
         */

        [HttpPost("login")]
        public async Task<ActionResult> VerifyLogin([FromBody]LoginWebRequest login)
        {
            Guid? accountId = await _accountService.VerifyLogin(login);

            // login successful
            if (accountId != null)
            {
                return Ok(accountId);
            }

            // login unsuccessful
            return BadRequest();
        }


        /* 
         * DELETE: api/Accounts/11111111-1111-1111-1111-111111111111
         * Return:
         * Ok(1) - Account successfully deleted
         * BadRequest(1) - Account failed to delete
         * NotFound(1) - Account does not exist
         * 
        */
        [HttpDelete("{accountId}")]
        public async Task<IActionResult> DeleteAccount(Guid accountId)
        {

            Account? account = await _accountService.GetAccountByAccountId(accountId);
            if (account == null)
            {
                return NotFound(1);
            }

            if (await _accountService.DeleteAccount(account))
            {
                return Ok(1);
            }

            return BadRequest(1);
        }


        /* 
         * Test Function for 2FA
         * GET: api/Accounts/security/get-qr-response
         * Return:
         * Ok(list) - List containing QR auth code, QR image URL, and QR manual entry key
         * BadRequest() - 2FA generation failed
        */
        [HttpGet("security/get-qr-response")]
        public async Task<ActionResult<List<QrResponse>>> GetQRResponse(Guid accountId)
        {
            QrResponse response = _twoFactorAuthService.GenerateTwoFactorAuthentication(accountId);
            List<QrResponse> list = new()
            {
                response
            };

            if (list.Count() != 0)
                return Ok(list);
            return BadRequest();
        }

        /* 
         * POST: api/Accounts/security/validate-2fa-pin
         * Return: 
         * Ok(true) - 2FA Pin validated successfully
         * BadRequest(false) - 2FA Pin is incorrect
        */
        [HttpPost("security/validate-2fa-pin")]
        public async Task<ActionResult<bool>> ValidateTwoFactorPIN([FromBody] ValidatePinWebRequest validatePin)
        {
            bool response = _twoFactorAuthService.ValidateTwoFactorPIN(validatePin);

            if (response)
                return Ok(response);
            return BadRequest(response);
        }

        /* 
         * GET: api/Accounts/security/get-2fa-flag/11111111-1111-1111-1111-111111111111
         * Return: 
        */
        [HttpGet("security/get-2fa-flag")]
        public async Task<ActionResult<bool>> GetTwoFactorFlag(Guid accountId)
        {
            bool? response = await _accountService.GetTwoFactorFlag(accountId);

            if (response == null)
            {
                return NotFound(1);
            }

            return Ok(response);
        }
    }
}
