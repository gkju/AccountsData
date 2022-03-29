using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using AccountsData.Data;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using ImageMagick;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using nClam;

namespace AccountsData.Models.DataModels
{
    public partial class ApplicationUser
    {
        public long UsedBytes { get; set; }
        public long MaxBytes { get; set; } = 1000000000;

        public List<File> Files { get; set; } = new ();
        public List<Folder> Folders { get; set; } = new();

        public bool MayUpload(long FileSizeInBytes)
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
            
            if (!MayUpload(file.Length))
            {
                throw new ArgumentException("Attempted to upload file larger than user's leftover space");
            }
            
            var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            
            var clam = new ClamClient(clamConfig.Host, clamConfig.Port);

            var fileStream = file.OpenReadStream();
            var stream = await PreprocessFile(fileStream, clam, file.ContentType);
            var owner = await dbContext.Users.FindAsync(this.Id);

            var minioFile = new File
            {
                Bucket = bucket,
                ByteSize = stream is not null ? stream.Length : file.Length,
                ObjectId = Guid.NewGuid().ToString(),
                Owner = owner,
                FileName = file.FileName,
                ContentType = file.ContentType,
                Public = isPublic,
                UserManageable = userManageable
            };
            owner.Files.Add(minioFile);

            PutObjectRequest uploadRequest = new PutObjectRequest()
            {
                InputStream = stream ?? file.OpenReadStream(),
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

        private async Task<Stream?> PreprocessFile(Stream fileStream, ClamClient clam, string ContentType)
        {
            var scanResult = await clam.SendAndScanFileAsync(fileStream);
            fileStream.Seek(0, SeekOrigin.Begin);

            switch (scanResult.Result)
            {
                case ClamScanResults.VirusDetected:
                    throw new ArgumentException("Virus detected!");
                case ClamScanResults.Error:
                    throw new ArgumentException("Clam error");
            }

            if (ContentType.StartsWith("image"))
            {
                using var image = new MagickImage(fileStream);
                image.Strip();
                image.AutoOrient();

                var stream = new MemoryStream();
                
                await image.WriteAsync(stream);
                stream.Seek(0, SeekOrigin.Begin);
                return stream;
            }

            return null;
        }
    }
}