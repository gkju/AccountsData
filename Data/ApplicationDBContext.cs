using AccountsData.Models.DataModels;
using AccountsData.Models.DataModels.Abstracts;
using AccountsData.Models.DataModels.Implementations.Properties;
using AccountsData.Models.DataModels.Implementations.Roles;
using AccountsData.Models.DataModels.Implementations.RoleScope;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AccountsData.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Role> BoardsRoles { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<Thread> Threads { get; set; }
        public DbSet<Scope> Scopes { get; set; }
        public DbSet<File> Files { get; set; }
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //all derived classes are added one by one as ef core entities
            builder.Entity<AuthorityProperty>().ToTable("AuthorityProperties");
            builder.Entity<BannedProperty>().ToTable("BannedProperties");
            builder.Entity<MayManageRolesProperty>().ToTable("MayManageRoleProperties");
            builder.Entity<MemberProperty>().ToTable("MemberProperties");
            builder.Entity<Prefixes>().ToTable("Prefixes");
            builder.Entity<EditOrDeleteProperty>().ToTable("EditOrDeleteProperty");
            builder.Entity<ReadProperty>().ToTable("ReadProperty");
            builder.Entity<ViewProperty>().ToTable("ViewProperty");
            builder.Entity<WriteProperty>().ToTable("WriteProperty");
            builder.Entity<AdminProperty>().ToTable("AdminProperty");
            builder.Entity<BanUsersProperty>().ToTable("BanUsersProperty");
            builder.Entity<BannedRole>();
            builder.Entity<BoardRole>();
            builder.Entity<GlobalRole>();
            builder.Entity<BoardScope>();
            builder.Entity<GlobalBoardScope>();
            builder.Entity<GlobalScope>();
            builder.Entity<NewsScope>();

            base.OnModelCreating(builder);
        }
    }
}