using System.ComponentModel.DataAnnotations;

namespace AccountsData.Models.DataModels;

public enum VideoType
{
    HLS_NORMAL
}

public class Video
{
    [Key]
    public string Id { get; set; }
    
    public VideoType Type { get; set; }

    public File MasterPlaylist { get; set; }
}