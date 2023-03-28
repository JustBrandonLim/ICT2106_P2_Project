using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using SmartHomeManager.Domain.AccountDomain.DTOs;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.AccountDomain.Interfaces;
using System.Net;

namespace SmartHomeManager.Domain.AccountDomain.Services
{
	public class AccountWriteService: IAccountWriteService
	{
		private readonly IAccountRepository _accountRepository;
        private readonly IAccountPasswordHashService _accountPasswordHashService;

        public AccountWriteService(IAccountRepository accountRepository, IAccountPasswordHashService accountPasswordHashService)
        {
            _accountRepository = accountRepository;
            _accountPasswordHashService = accountPasswordHashService;
        }
        public async Task<int> CreateAccount(AccountWebRequest accountWebRequest)
		{
			bool isEmailUnique = await _accountRepository.IsEmailUnique(accountWebRequest.Email);

			if (!isEmailUnique)
			{
				return 3;
			}

			// Create new Account object and assign the web request variables to it, except for the password
            Account realAccount = new Account();
			realAccount.AccountId = Guid.NewGuid();
            realAccount.Address = accountWebRequest.Address;
            realAccount.Email = accountWebRequest.Email;
            realAccount.Timezone = accountWebRequest.Timezone;
            realAccount.Username = accountWebRequest.Username;

            //Hash the password of the user using the newly created Guid as the salt
			realAccount.Password = _accountPasswordHashService.GetHashedPassword(realAccount.AccountId, accountWebRequest.Password);

            bool addAccountResponse = await _accountRepository.AddAsync(realAccount);
			if (addAccountResponse)
			{
				return await _accountRepository.SaveAsync();
			}

			else
			{
				// if account cannot be added
				return 2;
			}
		}

        public async Task<bool> UpdatePassword(PasswordWebRequest passwordWebRequest)
        {
            //Get the account based on the account id
            Account? account = await _accountRepository.GetByIdAsync(passwordWebRequest.AccountID);
            if (account != null)
            {
                //Update password
                account.Password = _accountPasswordHashService.GetHashedPassword(passwordWebRequest.AccountID, passwordWebRequest.Password);

                int updateResponse = await _accountRepository.Update(account);
                if (updateResponse == 1)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> UpdateTwoFactorFlag(Guid accountId, bool twoFactorFlag)
        {
            //Get the account based on the account id
            Account? account = await _accountRepository.GetByIdAsync(accountId);
            if (account != null)
            {
                //Update password
                account.TwoFactorFlag = twoFactorFlag;

                int updateResponse = await _accountRepository.Update(account);
                if (updateResponse == 1)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> UpdateAccount(Account account, AccountWebRequest accountWebRequest)
        {
            account.Email = accountWebRequest.Email;
            account.Username = accountWebRequest.Username;
            account.Address = accountWebRequest.Address;
            account.Timezone = accountWebRequest.Timezone;
            account.Password = _accountPasswordHashService.GetHashedPassword(account.AccountId, accountWebRequest.Password);
            account.DevicesOnboarded = accountWebRequest.DevicesOnboarded;

            int updateResponse = await _accountRepository.Update(account);
            if (updateResponse == 1)
            {
				return true;
            }

            return false;
        }

        public async Task<bool> DeleteAccount(Account account)
        {
			bool deleteResponse = _accountRepository.Delete(account);
			if (deleteResponse)
			{
				await _accountRepository.SaveAsync();
				return true;
			}

			return false;
        }
    }
}
