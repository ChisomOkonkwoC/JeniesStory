﻿using JeniesStory.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeniesStory.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<News> NewsArticles { get; set; }
        public DbSet<Story> Stories { get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);           

            modelBuilder.Entity<Story>()
                .HasOne(s => s.Author)
                .WithMany(a => a.Stories)
                .HasForeignKey(s => s.AuthorId)
                .OnDelete(DeleteBehavior.ClientCascade);
            

            modelBuilder.Entity<Bookmark>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bookmarks)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Bookmark>()
                .HasOne(b => b.Story)
                .WithMany(s => s.Bookmarks)
                .HasForeignKey(b => b.StoryId)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Comment>()
                .HasOne(b => b.Story)
                .WithMany(s => s.Comments)
                .HasForeignKey(b => b.StoryId)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Comment>()
               .HasOne(b => b.User)
               .WithMany(s => s.Comments)
               .HasForeignKey(b => b.UserId)
               .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Comment>()
               .HasOne(b => b.Author)
               .WithMany(s => s.Comments)
               .HasForeignKey(b => b.AuthorId)
               .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Comment>()
               .HasOne(b => b.Admin)
               .WithMany(s => s.ApprovedComments)
               .HasForeignKey(b => b.AdminId)
               .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Author>()
               .HasOne(b => b.User)
               .WithMany(s => s.Authors)
               .HasForeignKey(b => b.UserId)
               .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Admin>()
               .HasOne(b => b.User)
               .WithMany(s => s.Admins)
               .HasForeignKey(b => b.UserId)
               .OnDelete(DeleteBehavior.ClientCascade);

        }
    }
}