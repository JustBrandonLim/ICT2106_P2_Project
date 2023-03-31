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
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.DeviceDomain.Interfaces.Provides;
using SmartHomeManager.Domain.RoomDomain.Entities;
using SmartHomeManager.Domain.RoomDomain.Interfaces;

namespace SmartHomeManager.API.Controllers.AccountAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        // dependency injection is just saying this class can use this thing
        // in this context - accounts controller can use account service
        private readonly IAccountReadService _accountReadService;
        private readonly IAccountWriteService _accountWriteService;
        private readonly IEmailService _emailService;
        private readonly ITwoFactorAuthService _twoFactorAuthService;
        private readonly IDeviceInformationService _deviceInformationService;
        private readonly IRoomInformationService _roomInformationService;

        public AccountsController(IAccountReadService accountReadService, IAccountWriteService accountWriteService, 
            IEmailService emailService, ITwoFactorAuthService twoFactorAuthService, 
            IDeviceInformationService deviceInformationService, IRoomInformationService roomInformationService)
        {
            _accountReadService = accountReadService;
            _accountWriteService = accountWriteService;
            _emailService = emailService;
            _twoFactorAuthService = twoFactorAuthService;
            _deviceInformationService = deviceInformationService;
            _roomInformationService = roomInformationService;
        }

        /* 
         * GET: api/Accounts
         * Return: 
         * Ok(accounts) - IEnumerable
         * of accounts
         * NotFound(1) - No accounts in DB
        */
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
            IEnumerable<Account> accounts = await _accountReadService.GetAccounts();

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
            Account? account = await _accountReadService.GetAccountByAccountId(accountId);

            if (account == null)
            {
                return NotFound(1);
            }

            return Ok(account);
        }

        [HttpGet("{accountIdDevice}")]
        public async Task<ActionResult<IEnumerable<Device>>> GetDevicesByAccountId(Guid accountIdDevice)
        {
            IEnumerable<Device> devices = await _deviceInformationService.GetAllDevicesByAccountAsync(accountIdDevice);

            if (!devices.Any())
            {
                return NotFound(1);
            }

            return Ok(devices);
        }

        [HttpGet("{accountIdRoom}")]
        public async Task<ActionResult<IEnumerable<Room>>> GetRoomsByAccountId(Guid accountIdRoom)
        {
            IList<IRoom> rooms = _roomInformationService.GetRoomsByAccountId(accountIdRoom);

            if (!rooms.Any())
            {
                return NotFound(1);
            }

            return Ok(rooms);
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
            Account? account = await _accountReadService.GetAccountByAccountId(accountId);
            if (account == null)
            {
                return NotFound(1);
            }

            if (await _accountWriteService.UpdateAccount(account, accountWebRequest))
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
            int response = await _accountWriteService.CreateAccount(accountWebRequest);

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
         * Ok(loginResponse) - Login successful
         * BadRequest() - Login unsuccessful
         */

        [HttpPost("login")]
        public async Task<ActionResult<Account>> VerifyLogin([FromBody]LoginWebRequest login)
        {
            LoginResponse? loginResponse = await _accountReadService.VerifyLogin(login);

            // login successful
            if (loginResponse != null)
            {
                return Ok(loginResponse);
            }
            // login unsuccessful
            return BadRequest();
        }

        /*
         * POST: api/Accounts/passwordVerification
         * Return:
         * Ok() - Password Match
         * BadRequest() - Password don't match
         */

        [HttpPost("passwordVerification")]
        public async Task<ActionResult> VerifyPassword([FromBody] PasswordWebRequest passwordWebRequest)
        {
            bool verified = await _accountReadService.VerifyPassword(passwordWebRequest);

            // password match
            if (verified)
            {
                return Ok();
            }

            // password dont match
            return BadRequest();

        }

        /* 
         * PUT: api/Accounts/updatePassword/
         * Return:
         * Ok() - Account successfully updated
         * BadRequest() - Account failed to update
         * 
        */
        [HttpPut("updatePassword")]
        public async Task<IActionResult> PutNewPassword([FromBody] PasswordWebRequest passwordWebRequest)
        {

            if (await _accountWriteService.UpdatePassword(passwordWebRequest))
            {
                return Ok();
            }

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

            Account? account = await _accountReadService.GetAccountByAccountId(accountId);
            if (account == null)
            {
                return NotFound(1);
            }

            if (await _accountWriteService.DeleteAccount(account))
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
            bool? response = await _accountReadService.GetTwoFactorFlag(accountId);

            if (response == null)
            {
                return NotFound(1);
            }

            return Ok(response);
        }

        /* 
         * PUT: api/Accounts/security/update-2fa-flag/
         * Return: 
        */
        [HttpPut("security/update-2fa-flag")]
        public async Task<ActionResult<bool>> GetTwoFactorFlag(Guid accountId, bool twoFactorFlag)
        {
            bool? response = await _accountWriteService.UpdateTwoFactorFlag(accountId, twoFactorFlag);

            if (response == null)
            {
                return NotFound(1);
            }

            return Ok(response);
        }
    }
}
