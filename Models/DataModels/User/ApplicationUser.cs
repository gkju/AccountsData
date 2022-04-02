using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace AccountsData.Models.DataModels
{
    public partial class ApplicationUser : IdentityUser
    {
        public ProfilePicture? ProfilePicture { get; set; }
        
        public double SocialCredit { get; set; }
        
        public List<Fido2Pk>? FidoCredentials { get; set; }
    }
}