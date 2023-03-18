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
	public class AccountReadService : IAccountReadService, IAccountPasswordHashService
	{
		private readonly IAccountRepository _accountRepository;

		public AccountReadService(IAccountRepository accountRepository)
		{
			_accountRepository = accountRepository;
		}

		public async Task<Account?> GetAccountByAccountId(Guid id)
		{
			Account? account = await _accountRepository.GetByIdAsync(id);

			if (account == null)
			{
				return null;
			}

			return account;
		}

        public async Task<bool?> GetTwoFactorFlag(Guid id)
        {
            Account? account = await _accountRepository.GetByIdAsync(id);

            if (account == null)
            {
                return null;
            }

            return account.TwoFactorFlag;
        }

        public async Task<IEnumerable<Account>> GetAccounts()
		{
			IEnumerable<Account> accounts = await _accountRepository.GetAllAsync();

			if (accounts == null)
			{
				return Enumerable.Empty<Account>();
			}

			return accounts;
		}

		public async Task<LoginResponse?> VerifyLogin(LoginWebRequest login)
		{
			Account? account = await _accountRepository.GetAccountByEmailAsync(login.Email);
            //Hash the password of the user using the newly created Guid as the salt
            
            if (account != null)
			{
				login.Password = GetHashedPassword(account.AccountId, login.Password);

                if (account.Password == login.Password)
				{
                    // account exists and password is correct
                    // return 1;
                    LoginResponse newLoginResponse = new()
                    {
                        AccountId = account.AccountId,
						Email = account.Email,
						Username = account.Username,
                        TwoFactorFlag = account.TwoFactorFlag
                    };
                    return newLoginResponse;
				}
			}

            // account does not exist/account exists but password is wrong
            return null;
		}

        public async Task<bool> VerifyPassword(PasswordWebRequest passwordWebRequest)
        {
            //Get the account based on the account id
            Account? account = await _accountRepository.GetByIdAsync(passwordWebRequest.AccountID);


            if (account != null)
            {
                //Hash the password that user input
                string hashedPassword = GetHashedPassword(passwordWebRequest.AccountID, passwordWebRequest.Password);

                // Check if password match
                if (account.Password == hashedPassword)
                {
                    return true;
                }
            }

            // account does not exist/account exists but password is wrong
            return false;
        }

		public string GetHashedPassword(Guid accountId, string unhashedPassword)
		{
			string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
				password: unhashedPassword,
				salt: accountId.ToByteArray(),
				prf: KeyDerivationPrf.HMACSHA256,
				iterationCount: 100000,
				numBytesRequested: 256 / 8));

			return hashedPassword;
        }
    }
}
