using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Fido2NetLib.Development;
using Fido2NetLib.Objects;

namespace AccountsData.Models.DataModels;

public class Fido2Pk
{
    [Key] 
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    public string OwnerId { get; set; }
    public ApplicationUser Owner { get; set; }
    
    public StoredCredentialId StoredCredential { get; set; }
}

public class StoredCredentialId : StoredCredential
{
    [Key] 
    public string Id { get; set; } = Guid.NewGuid().ToString();
}