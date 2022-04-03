using AccountsData.Models.DataModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AccountsData.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Board> Boards { get; set; }
        public DbSet<Thread> Threads { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<ProfilePicture> ProfilePictures { get; set; }
        public DbSet<EmailChangeRequest> EmailChangeRequests { get; set; }
        
        public DbSet<Fido2Pk> FidoCredentials { get; set; }
        
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>()
                .HasOne<ProfilePicture>()
                .WithOne(p => p.Owner);

            builder.Entity<File>()
                .HasOne(f => f.MasterFile)
                .WithMany(f => f.ChildrenFiles)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<File>()
                .HasOne(f => f.Parent)
                .WithMany(f => f.Files)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Folder>()
                .HasOne(f => f.MasterFolder)
                .WithMany(f => f.SubFolders)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(builder);
        }
    }
}