using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.AccountDomain.DTOs
{
    public class ValidatePinWebRequest
    {
        public String AuthenticationCode { get; set; }
        public String Pin { get; set; }
    }
}
