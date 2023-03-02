using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.AccountDomain.DTOs
{
    public class PasswordWebRequest
    {

        [Required]
        public Guid AccountID { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
