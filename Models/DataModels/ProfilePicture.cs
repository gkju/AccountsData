using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountsData.Models.DataModels;

public class ProfilePicture
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    
    public File Picture { get; set; }
    
    public string OwnerId { get; set; }
    public ApplicationUser Owner { get; set; }
}