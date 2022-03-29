namespace AccountsData.Models.DataModels;

public class ProfilePicture
{
    public string Id { get; set; }
    
    public File? Picture { get; set; }
    
    public string OwnerId { get; set; }
    public ApplicationUser Owner { get; set; }
}