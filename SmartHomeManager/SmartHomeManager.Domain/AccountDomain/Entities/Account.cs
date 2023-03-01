using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.AccountDomain.Entities
{
    public class Account
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid AccountId { get; set; }

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

        [Required]
        public bool TwoFactorFlag { get; set; }
    }
}
