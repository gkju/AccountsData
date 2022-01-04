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
        public DbSet<EmailChangeRequest> EmailChangeRequests { get; set; }
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
    }
}