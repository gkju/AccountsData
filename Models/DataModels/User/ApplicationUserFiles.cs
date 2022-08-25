using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using AccountsData.Data;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using nClam;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;

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
        public async Task<string> UploadFile(IFormFile file, IServiceProvider serviceProvider, string bucket, bool isPublic = false, bool userManageable = true)
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
            var minioClient = serviceProvider.GetRequiredService<AmazonS3Client>();
            var clamConfig = serviceProvider.GetRequiredService<ClamConfig>();
            
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
                BucketName = minioFile.Bucket,
                ContentType = file.ContentType,
            };

            await minioClient.PutObjectAsync(uploadRequest);
            minioFile.BackedInMinio = true;
            await dbContext.Files.AddAsync(minioFile);
            UsedBytes += minioFile.ByteSize;
            await dbContext.SaveChangesAsync();
            return minioFile.ObjectId;
        }

        public async Task DeleteFile(string fileId, IServiceProvider serviceProvider)
        {
            var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var file = Files.First(f => f.ObjectId == fileId);
            if (file.UserManageable)
            {
                dbContext.Remove(file);
                await dbContext.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("File is not user manageable");
            }
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
            
            // only process raster images and avif chads do not get image processing yet. sad.
            if (ContentType.StartsWith("image/svg") || ContentType.StartsWith("image/avif"))
            {
                return fileStream;
            }

            if (ContentType.StartsWith("image"))
            {
                using var image = Image.Load(fileStream);

                image.Mutate(x => x.AutoOrient());
                image.Metadata.ExifProfile = null;
                image.Metadata.XmpProfile = null;

                var stream = new MemoryStream();
                
                await image.SaveAsync(stream, new WebpEncoder());
                stream.Seek(0, SeekOrigin.Begin);
                return stream;
            }

            return null;
        }

        public void DeleteProfilePicture(ApplicationDbContext dbContext)
        {
            dbContext.Files.Remove(ProfilePicture.Picture);
            dbContext.ProfilePictures.Remove(ProfilePicture);
        }
    }
}