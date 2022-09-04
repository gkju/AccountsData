using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Fido2NetLib;
using Microsoft.AspNetCore.Identity;

namespace AccountsData.Models.DataModels
{
    public partial class ApplicationUser : IdentityUser
    {
        public ProfilePicture? ProfilePicture { get; set; }
        
        public double SocialCredit { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public List<Fido2Pk> FidoCredentials { get; set; } = new ();
        public List<Article> Articles { get; set; } = new();
        
        [System.Text.Json.Serialization.JsonIgnore]
        public string? AssertionOptionsJson { get; set; }
        
        [System.Text.Json.Serialization.JsonIgnore]
        public string? AttestationOptionsJson { get; set; }
        
        [System.Text.Json.Serialization.JsonIgnore]
        public override string? PasswordHash { get; set; }
        
        [System.Text.Json.Serialization.JsonIgnore]
        public override string? ConcurrencyStamp { get; set; }
        
        [System.Text.Json.Serialization.JsonIgnore]
        public override string? SecurityStamp { get; set; }

        public static implicit operator Fido2User (ApplicationUser user)
        {
            return new Fido2User
            {
                Name = user.UserName,
                DisplayName = user.UserName,
                Id = Encoding.UTF8.GetBytes(user.Id)
            };
        }
    }
}