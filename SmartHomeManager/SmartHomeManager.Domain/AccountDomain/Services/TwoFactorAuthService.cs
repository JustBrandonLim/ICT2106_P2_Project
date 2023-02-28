using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Authenticator;

namespace SmartHomeManager.Domain.AccountDomain.Services
{
    public class TwoFactorAuthService
    {
        public TwoFactorAuthService() { }
        String? AuthenticationCode
        {
            get;
            set;
        }

        String? AuthenticationTitle
        {
            get
            {
                return "Juleus";
            }
        }


        String? AuthenticationBarCodeImage
        {
            get;
            set;
        }

        String? AuthenticationManualCode
        {
            get;
            set;
        }
  
        public Boolean ValidateTwoFactorPIN(String pin)  
        {  
            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();  
            return tfa.ValidateTwoFactorPIN(AuthenticationCode, pin);  
        }  
  
        public async Task<IEnumerable<String>> GenerateTwoFactorAuthentication()  
        {  
            Guid guid = new Guid("11111111-1111-1111-1111-111111111111");  
            String uniqueUserKey = Convert.ToString(guid).Replace("-", "").Substring(0, 10);  
            AuthenticationCode = uniqueUserKey;
            Console.WriteLine("Auth Code: " + AuthenticationCode);
  
            Dictionary<String, String> result = new Dictionary<String, String>();  
            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();  
            var setupInfo = tfa.GenerateSetupCode("Complio", AuthenticationTitle, AuthenticationCode, false, 300);
            List<String> response = new List<String>();

            if (setupInfo != null)  
            {  
                
                AuthenticationBarCodeImage = setupInfo.QrCodeSetupImageUrl; 
                Console.WriteLine("Auth Image URL: " + AuthenticationBarCodeImage);
                AuthenticationManualCode = setupInfo.ManualEntryKey;  
                Console.WriteLine("Auth Manual Code: " + AuthenticationManualCode);

                response.Add(AuthenticationCode);
                response.Add(AuthenticationBarCodeImage);
                response.Add(AuthenticationManualCode);
            }
            return response;
        }
    }
}
