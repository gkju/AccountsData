using System;
using System.ComponentModel.DataAnnotations;

namespace AccountsData.Models.DataModels;

public class EmailChangeRequest
{
    [Key] 
    public Guid Id { get; set; }
    
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

    public string Email { get; set; }

    public string CodeHash { get; set; }

    public DateTime Date { get; set; }
    public bool Realized { get; set; }

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