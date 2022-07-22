using System.Collections.Generic;
using AccountsData.Models.DataModels;
using AccountsData.Models.DataModels.Implementations.Properties;
using AccountsData.Models.DataModels.Implementations.Roles;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AccountsData.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Properties> Properties { get; set; }
        
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

            builder.Entity<AdminProperty>()
                .Property(p => p.Data)
                .HasColumnName("BoolData");
            builder.Entity<AuthorityProperty>()
                .Property(p => p.Data)
                .HasColumnName("IntData");
            builder.Entity<BannedProperty>()
                .Property(p => p.Data)
                .HasColumnName("BoolData");
            builder.Entity<BanUsersProperty>()
                .Property(p => p.Data)
                .HasColumnName("BoolData");
            builder.Entity<EditOrDeleteProperty>()
                .Property(p => p.Data)
                .HasColumnName("BoolData");
            builder.Entity<MayManageRolesProperty>()
                .Property(p => p.Data)
                .HasColumnName("BoolData");
            builder.Entity<MemberProperty>()
                .Property(p => p.Data)
                .HasColumnName("BoolData");
            builder.Entity<Prefixes>()
                .Property(p => p.Data)
                .HasColumnName("PrefixData")
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<HashSet<Prefix>>(v));
            builder.Entity<ReadProperty>()
                .Property(p => p.Data)
                .HasColumnName("BoolData");
            builder.Entity<ViewProperty>()
                .Property(p => p.Data)
                .HasColumnName("BoolData");
            builder.Entity<WriteProperty>()
                .Property(p => p.Data)
                .HasColumnName("BoolData");
            base.OnModelCreating(builder);
        }
    }
}