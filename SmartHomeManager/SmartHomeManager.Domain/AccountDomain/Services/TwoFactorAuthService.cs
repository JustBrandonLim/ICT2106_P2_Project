using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Authenticator;
using SmartHomeManager.Domain.AccountDomain.DTOs;

namespace SmartHomeManager.Domain.AccountDomain.Services
{
    public class TwoFactorAuthService
    {
        public TwoFactorAuthService() { }
  
        public async Task<Boolean> ValidateTwoFactorPIN(ValidatePinWebRequest validatePin)  
        {  
            TwoFactorAuthenticator tfa = new();
            String authCode = validatePin.AuthenticationCode.Replace("-", "");
            return tfa.ValidateTwoFactorPIN(authCode, validatePin.Pin);  
        }  
  
        public async Task<QrResponse> GenerateTwoFactorAuthentication(Guid accountId)
        {  
            String accountIdString = accountId.ToString();
            String authCode = accountIdString.Replace("-", ""); 
            /*Console.WriteLine("Auth Code: " + authCode);*/

            TwoFactorAuthenticator tfa = new();
            var setupInfo = tfa.GenerateSetupCode("Company", "Placeholder", authCode, false, 5);
            QrResponse response = new();

            if (setupInfo != null) 
            {  
                /*Console.WriteLine("Auth Image URL: " + setupInfo.QrCodeSetupImageUrl);
                Console.WriteLine("Auth Manual Code: " + setupInfo.ManualEntryKey);*/

                response.AuthenticationCode = authCode;
                response.AuthenticationBarCodeImage = setupInfo.QrCodeSetupImageUrl;
                response.AuthenticationManualCode = setupInfo.ManualEntryKey;
            }
            return response;
        }
    }
}
