using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using AccountsData.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Identity;
using Minio;

namespace AccountsData.Models.DataModels
{
    public partial class ApplicationUser
    {
        public UInt64 UsedBytes { get; set; }
        public UInt64 MaxBytes { get; set; } = 1000000000;

        public bool MayUpload(UInt64 FileSizeInBytes)
        {
            return FileSizeInBytes + UsedBytes <= MaxBytes;
        }

        //no di go brrrrrrrrrrrrrrrrrrrrrrr
        public async Task<string> UploadFile(IFormFile file, ApplicationDbContext dbContext, MinioClient minioClient, string bucket, bool isPublic)
        {
            if (file.Length < 0)
            {
                throw new ArgumentException("File length < 0, ???");
            }
            
            if (!MayUpload((UInt64) file.Length))
            {
                throw new ArgumentException("Attempted to upload file larger than user's leftover space");
            }

            var minioFile = new File
            {
                Bucket = bucket,
                ByteSize = (UInt64) file.Length,
                ObjectId = Guid.NewGuid().ToString(),
                Owner = this,
                OwnerId = Id,
                FileName = file.FileName,
                ContentType = file.ContentType,
                Public = isPublic
            };
            
            await minioClient.PutObjectAsync(minioFile.Bucket, minioFile.ObjectId, file.OpenReadStream(), file.Length, minioFile.ContentType);
            await dbContext.Files.AddAsync(minioFile);
            UsedBytes += minioFile.ByteSize;
            await dbContext.SaveChangesAsync();
            return minioFile.ObjectId;
        }
    }
}