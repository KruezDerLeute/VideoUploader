using aspIdentity.Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace aspIdentity.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options)
        {
            
        }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<YoutubeLinks> Youtubes { get; set; }
        public DbSet<VideoComment> VideoComments { get; set; }
        public DbSet<YoutubeLinkComment> YoutubeLinkComments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Define the relationships between entities (tables)

            // Relationship between AppUser and Video (one-to-many)
            builder.Entity<AppUser>()
                .HasMany(user => user.Videos)
                .WithOne(video => video.User)
                .HasForeignKey(video => video.UserId);

            // Relationship between AppUser and YoutubeLinks (one-to-many)
            builder.Entity<AppUser>()
                .HasMany(user => user.YoutubeLinks)
                .WithOne(y => y.User)
                .HasForeignKey(y => y.UserId);

            // Relationship between AppUser and VideoComment (one-to-many)
            builder.Entity<AppUser>()
                .HasMany(user => user.Comments)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId);

            // Relationship between AppUser and YoutubeLinkComment (one-to-many)
            builder.Entity<AppUser>()
                .HasMany(user => user.YTComments)
                .WithOne(comment => comment.User)
                .HasForeignKey(comment => comment.UserId);

            // Relationship between VideoComment and Video (one-to-many)
            builder.Entity<Video>()
                .HasMany(c => c.Comments)
                .WithOne(c => c.Video)
                .HasForeignKey(c => c.VideoId);

            // Relationship between YoutubeLinkComment and YoutubeLinks (one-to-many)
            builder.Entity<YoutubeLinks>()
                .HasMany(y => y.YTComments)
                .WithOne(y => y.YoutubeLink)
                .HasForeignKey(c => c.YoutubeLinkEntityId);
        }
    }
}
