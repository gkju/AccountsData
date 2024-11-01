﻿using System;
using System.Collections.Generic;
using System.Linq;
using AccountsData.Models.DataModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;

namespace AccountsData.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Board> Boards { get; set; }
        public DbSet<Thread> Threads { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<Article> Articles { get; set; }
        
        public DbSet<ProfilePicture> ProfilePictures { get; set; }
        public DbSet<EmailChangeRequest> EmailChangeRequests { get; set; }
        
        public DbSet<Fido2Pk> FidoCredentials { get; set; }
        
        public DbSet<Tag> Tags { get; set; }
        
        public DbSet<Comment> Comments { get; set; }
        
        public DbSet<Reaction> Reactions { get; set; }
        
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>()
                .HasOne<ProfilePicture>()
                .WithOne(p => p.Owner);

            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Articles);

            builder.Entity<Article>()
                .HasOne(a => a.Author);
            builder.Entity<Article>()
                .HasMany(a => a.Editors);
            builder.Entity<Article>()
                .HasMany(a => a.Reviewers);
            builder.Entity<Article>()
                .HasMany(a => a.Reactions);
            
            builder.Entity<Folder>()
                .HasOne(f => f.MasterFolder);
            builder.Entity<Folder>()
                .HasMany(f => f.Files);
            base.OnModelCreating(builder);
        }
    }
}