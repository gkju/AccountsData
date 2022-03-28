using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using AccountsData.Data;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using nClam;

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
        public async Task<string> UploadFile(IFormFile file, IServiceProvider serviceProvider, AmazonS3Client minioClient, ClamConfig clamConfig, string bucket, bool isPublic = false, bool userManageable = true)
        {
            if (file.Length < 0)
            {
                throw new ArgumentException("File length < 0, ???");
            }
            
            if (!MayUpload((UInt64) file.Length))
            {
                throw new ArgumentException("Attempted to upload file larger than user's leftover space");
            }
            
            var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            
            var clam = new ClamClient(clamConfig.Host, clamConfig.Port);

            var fileStream = file.OpenReadStream();

            var scanResult = await clam.SendAndScanFileAsync(fileStream);
            fileStream.Seek(0, SeekOrigin.Begin);

            switch (scanResult.Result)
            {
                case ClamScanResults.VirusDetected:
                    throw new ArgumentException("Virus detected!");
                case ClamScanResults.Error:
                    throw new ArgumentException("Clam error");
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
                Public = isPublic,
                UserManageable = userManageable
            };

            PutObjectRequest uploadRequest = new PutObjectRequest()
            {
                InputStream = file.OpenReadStream(),
                Key = minioFile.ObjectId,
                BucketName = minioFile.Bucket
            };

            await minioClient.PutObjectAsync(uploadRequest);
            minioFile.BackedInMinio = true;
            await dbContext.Files.AddAsync(minioFile);
            UsedBytes += minioFile.ByteSize;
            await dbContext.SaveChangesAsync();
            return minioFile.ObjectId;
        }
    }
}