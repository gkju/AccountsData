using Fido2NetLib.Objects;

namespace AccountsData.Models.DataModels;

public class Fido2Pk
{
    public string OwnerId { get; set; }
    public ApplicationUser Owner { get; set; }
    
    public PublicKeyCredentialDescriptor Descriptor { get; set; }
}