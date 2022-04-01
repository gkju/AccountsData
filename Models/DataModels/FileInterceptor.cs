using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AccountsData.Data;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace AccountsData.Models.DataModels;

public class FileInterceptor : SaveChangesInterceptor
{
    private AmazonS3Client minioClient { get; set; }
    
    public FileInterceptor(AmazonS3Client minioClient)
    {
        this.minioClient = minioClient;
    }
    
    private bool IsSameTypeOrSub(Type baseT, Type descT)
    {
        return baseT.IsAssignableFrom(descT) || baseT == descT;
    }
    
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        throw new NotImplementedException();
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in eventData.Context.ChangeTracker.Entries<File>().Where(e => e.State == EntityState.Deleted))
        {
            File entity = entry.Entity;

            if (!entity.BackedInMinio)
            {
                continue;
            }

            var deleteObjectRequest = new DeleteObjectRequest
            {
                BucketName = entity.Bucket,
                Key = entity.ObjectId
            };
            
            var context = (ApplicationDbContext) eventData.Context;

            try
            {
                var owner = await context.Users.FindAsync(new object?[] { entity.Owner.Id }, cancellationToken: cancellationToken);
                owner.UsedBytes -= entity.ByteSize;
                await eventData.Context.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                Console.WriteLine("Unable to free up user's bytes");
            }
            

            await minioClient.DeleteObjectAsync(deleteObjectRequest);
            
        }

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}