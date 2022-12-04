using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountsData.Models.DataModels
{
    public class Folder
    {
        [Key] public string Id { get; set; } = Guid.NewGuid().ToString();
        
        public string Name { get; set; }
        
        public string OwnerId{ get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public ApplicationUser Owner { get; set; }

        public string? MasterFolderId { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public Folder? MasterFolder { get; set; }
        public List<File> Files;
    }
}