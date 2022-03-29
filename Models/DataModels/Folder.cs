using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountsData.Models.DataModels
{
    public class Folder
    {
        [Key]
        public string Id { get; set; }
        
        [ForeignKey("Owner")]
        public string OwnerId{ get; set; }
        public ApplicationUser Owner { get; set; }

        public string? MasterFolderId { get; set; }
        public Folder? MasterFolder { get; set; }
        public List<Folder> SubFolders;
        public List<File> Files;
    }
}