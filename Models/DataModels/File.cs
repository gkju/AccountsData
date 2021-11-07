using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountsData.Models.DataModels
{
    public class File
    {
        public string Bucket { get; set; }
        
        [Key]
        public string ObjectId { get; set; }
        
        public bool Public { get; set; }
        
        public string FileName { get; set; }
        
        public string ContentType { get; set; }
        
        public ApplicationUser Owner { get; set; }
        [ForeignKey("Owner")]
        public string OwnerId{ get; set; }
        
        public UInt64 ByteSize { get; set; }
        
        public Folder Parent { get; set; }
        [ForeignKey("Parent")]
        public string ParentId{ get; set; }
    }
}