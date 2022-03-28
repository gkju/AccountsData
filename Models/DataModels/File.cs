using System;
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
        public string Bucket { get; set; }
        
        public bool BackedInMinio { get; set; }
        public bool UserManageable { get; set; }
        
        [Key]
        public string ObjectId { get; set; }
        
        public bool Public { get; set; }
        
        public string FileName { get; set; }
        
        public string ContentType { get; set; }
        
        public ApplicationUser Owner { get; set; }
        [ForeignKey("Owner")]
        public string OwnerId{ get; set; }
        
        public UInt64 ByteSize { get; set; }
        
        public Folder? Parent { get; set; }
        [ForeignKey("Parent")]
        public string? ParentId{ get; set; }
        
        // useful for hls fragments
        public File? MasterFile { get; set; }
        [ForeignKey("MasterFile")]
        public string? MasterFileId { get; set; }

        public GetObjectRequest GetFileRequest(MinioConfig minioConfig)
        {
            return new GetObjectRequest()
            {
                BucketName = minioConfig.BucketName,
                Key = ObjectId
            };
        }
    }
}