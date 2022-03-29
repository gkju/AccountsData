using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Net.Mime;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;

namespace AccountsData.Models.DataModels
{
    // files should only be abstracted by other classes
    public sealed class File
    {
        [Key]
        public string ObjectId { get; set; }
        
        public string Bucket { get; set; }
        
        public bool BackedInMinio { get; set; }
        public bool UserManageable { get; set; }

        public bool Public { get; set; }
        
        public string FileName { get; set; }
        
        public string ContentType { get; set; }
        
        [ForeignKey("Owner")]
        public string OwnerId { get; set; }
        public ApplicationUser Owner { get; set; }

        public long ByteSize { get; set; }
        
        [ForeignKey("Parent")]
        public string? ParentId{ get; set; }
        public Folder? Parent { get; set; }

        // useful for hls fragments
        [ForeignKey("MasterFile")]
        public string? MasterFileId { get; set; }
        public File? MasterFile { get; set; }
        public List<File> ChildrenFiles { get; set; }
    }
}