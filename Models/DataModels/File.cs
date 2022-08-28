using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Net.Mime;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Cppl.Utilities.AWS;

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

        public Stream GetSeekableStream(AmazonS3Client minioClient)
        {
            if (!BackedInMinio)
            {
                throw new Exception("Attempted to open file not backed in s3");
            }
            return new SeekableS3Stream(minioClient, Bucket, ObjectId);
        }

        public string GetSignedUrl(AmazonS3Client minioClient, TimeSpan? duration = null,
            ContentDisposition? contentDisposition = null, ContentType? contentType = null)
        {
            if (!BackedInMinio)
            {
                throw new Exception("Attempted to open file not backed in s3");
            }

            duration ??= TimeSpan.FromDays(7);

            var request = new GetPreSignedUrlRequest()
            {
                BucketName = Bucket,
                Key = ObjectId,
                Expires = DateTime.UtcNow.Add((TimeSpan) duration),
                Verb = HttpVerb.GET
            };
            
            contentDisposition ??= new ContentDisposition
            {
                FileName = FileName,
                Inline = false
            };
            contentType ??= new ContentType(ContentType);
            
            
            request.ResponseHeaderOverrides.ContentType = contentType.ToString();
            request.ResponseHeaderOverrides.ContentDisposition = contentDisposition.ToString();

            return minioClient.GetPreSignedURL(request);
        }
    }
}