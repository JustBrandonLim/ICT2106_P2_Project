using SmartHomeManager.Domain.AccountDomain.DTOs;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.AccountDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SmartHomeManager.Domain.AccountDomain.Factory
{
    internal class ProfileFactory
    {
        public static Profile makeProfile(ProfileWebRequest profileWebRequest)
        {
            //Get the variables needed
            string Name = profileWebRequest.Name;
            string Description = profileWebRequest.Description;
            int? Pin = profileWebRequest.Pin;
            Guid AccountId = profileWebRequest.AccountId;

            //Check which type to create
            if (Pin == null)
            {
                SimpleProfile simpleProfile = new SimpleProfile(Name, Description, AccountId);
                return new Profile
                {
                ProfileId = simpleProfile.ProfileId,
                Name = simpleProfile.Name,
                Description = simpleProfile.Description,
                Pin = simpleProfile.Pin,
                AccountId = simpleProfile.AccountId
                };
            }
            else
            {
                ParentalControlProfile parentalControlProfile = new ParentalControlProfile(Name, Description, AccountId, Pin);
                return new Profile
                {
                    ProfileId = parentalControlProfile.ProfileId,
                    Name = parentalControlProfile.Name,
                    Description = parentalControlProfile.Description,
                    Pin = parentalControlProfile.Pin,
                    AccountId = parentalControlProfile.AccountId
                };
            }
        }
    }
}
