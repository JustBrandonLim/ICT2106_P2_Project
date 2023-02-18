using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartHomeManager.DataSource;
using SmartHomeManager.DataSource.AccountDataSource;
using SmartHomeManager.Domain.AccountDomain.DTOs;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.AccountDomain.Interfaces;
using SmartHomeManager.Domain.AccountDomain.Services;


namespace SmartHomeManager.API.Controllers.AccountController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        // dependency injection is just saying this class can use this thing
        // in this context - accounts controller can use account service
        private readonly AccountService _accountService;
        private readonly EmailService _emailService;

        public AccountsController(AccountService accountService, EmailService emailService)
        {
            _accountService = accountService;
            _emailService = emailService;
        }

        /*private readonly AccountRepository _accountRepository;
        
        public AccountsController(AccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }*/

        /* 
         * GET: api/Accounts
         * Returns: 
        */
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
            IEnumerable<Account> accounts = await _accountService.GetAccounts();

            if (accounts == null)
            {
                return NotFound();
            }

            return Ok(accounts);
        }

        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccountByAccountId(Guid id)
        {
            var account = await _accountService.GetAccountByAccountId(id);

            if (account == null)
            {
                return NotFound();
            }

            return account;
        }


        /* 
         * PUT: api/Accounts/11111111-1111-1111-1111-111111111111
         * Return:
         * Ok(1) - Account successfully updated
         * BadRequest(1) - Account failed to update
         * NotFound(1) - Account does not exist
         * 
        */
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(Guid id, [FromBody] AccountWebRequest accountWebRequest)
        {
            var account = await _accountService.GetAccountByAccountId(id);
            if (account == null)
            {
                return NotFound(1);
            }

            if (await _accountService.UpdateAccount(id, accountWebRequest))
            {
                return Ok(1);
            }

            return BadRequest(1);
        }

        /* 
         * POST: api/Accounts
         * Return:
         * Ok(1) - Account Created & Email Sent
         * Ok(2) - Account Created but Email Not Sent
         * BadRequest(1) - Account Not Created
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
         * Ok(1) - Login Successful
         * BadRequest(1) - Login Unsuccessful, wrong password
         * BadRequest(2) - Login Unsuccessful, account does not exist
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(Guid id)
        {

            var account = await _accountService.GetAccountByAccountId(id);
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
    }
}
