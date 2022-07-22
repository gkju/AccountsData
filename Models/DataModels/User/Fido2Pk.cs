using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Fido2NetLib.Objects;

namespace AccountsData.Models.DataModels;

public class Fido2Pk
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public string Id { get; set; }
    
    public string OwnerId { get; set; }
    public ApplicationUser Owner { get; set; }
    
    public PublicKeyCredentialDescriptor Descriptor { get; set; }
}