using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.AccountDomain.DTOs
{
    public class ParentControlPin
    {
        [Required]
        public Guid ProfileId { get; set; }

        public int? Pin { get; set; }
    }

    public class ProfileIdRequest
    {
        [Required]
        public Guid ProfileId { get; set; }
    }
}
