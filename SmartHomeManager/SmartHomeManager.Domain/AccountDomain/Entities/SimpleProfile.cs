using SmartHomeManager.Domain.AccountDomain.DTOs;
using SmartHomeManager.Domain.AccountDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.AccountDomain.Entities
{
    internal class SimpleProfile : IProfile
    {
        public Guid ProfileId { get ; set; }
        public string Name { get ; set; }
        public string Description { get; set; }
        public int? Pin { get; set; }
        public Guid AccountId { get; set; }

        public SimpleProfile(string Name, string Description, Guid AccountId)
        {
            this.ProfileId = Guid.NewGuid();
            this.Name = Name;
            this.Description = Description;
            this.AccountId = AccountId;
            this.Pin = null;
        }
    }
}
