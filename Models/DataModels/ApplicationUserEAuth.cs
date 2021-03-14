using System;
using Microsoft.AspNetCore.Identity;

namespace AccountsData.Models.DataModels
{
    public partial class ApplicationUser : IdentityUser
    {
        public void HandleExternalAuth(UserLoginInfo info)
        {
            Console.WriteLine($"Handling external login, info {info}");
        }
    }
}