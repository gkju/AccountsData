using System;
using System.ComponentModel.DataAnnotations;

namespace AccountsData.Models.DataModels;

public class EmailChangeRequest
{
    [Key] public Guid Id { get; set; }
    [Required] public ApplicationUser User { get; set; }
    [Required] public string Email { get; set; }
    [Required] public string CodeHash { get; set; }
    [Required] public DateTime Date { get; set; }
    public bool Realised { get; set; }

    public EmailChangeRequest()
    {
        
    }
    
    public EmailChangeRequest(ApplicationUser user, string email, string codeHash)
    {
        this.User = user;
        this.Email = email;
        this.CodeHash = codeHash;
        Date = DateTime.Now.ToUniversalTime();
    }
}