using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.AccountDomain.DTOs
{
    public class QrResponse
    { 
        public string AuthenticationCode { get; set; }
        public string AuthenticationBarCodeImage { get; set; }
        public string AuthenticationManualCode { get; set; }
    }
}
