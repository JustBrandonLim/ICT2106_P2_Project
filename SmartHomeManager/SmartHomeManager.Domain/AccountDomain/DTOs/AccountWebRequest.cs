using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.AccountDomain.DTOs
{
    public class AccountWebRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public int Timezone { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public int DevicesOnboarded { get; set; }
    }
}
