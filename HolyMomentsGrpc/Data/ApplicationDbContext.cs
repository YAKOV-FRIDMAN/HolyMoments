using HolyMomentsGrpc.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace HolyMomentsGrpc.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>()
               .HasMany(u => u.Videos)
               .WithOne(v => v.User)
               .HasForeignKey(v => v.UserId)
               .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Comments)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Likes)
                .WithOne(l => l.User)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Notifications)
                .WithOne(n => n.User)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Video>()
                .HasMany(v => v.Comments)
                .WithOne(c => c.Video)
                .HasForeignKey(c => c.VideoId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Video>()
                .HasMany(v => v.Likes)
                .WithOne(l => l.Video)
                .HasForeignKey(l => l.VideoId)
                .OnDelete(DeleteBehavior.NoAction);
        }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Notification> Notifications { get; set; }
    }
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public ICollection<Video> Videos { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<Notification> Notifications { get; set; }
    }
}
