using Microsoft.EntityFrameworkCore;
using REST.Entity.Db;

namespace REST.Storage.Common
{
    public abstract class DbStorage : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Marker> Markers { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tweet> Tweets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasKey(a => a.Id);
            modelBuilder.Entity<Author>().Property(a => a.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Author>().Property(a => a.Password).IsRequired().HasMaxLength(128);
            modelBuilder.Entity<Author>().Property(a => a.FirstName).IsRequired().HasMaxLength(64);
            modelBuilder.Entity<Author>().Property(a => a.LastName).IsRequired().HasMaxLength(64);
            modelBuilder.Entity<Author>().Property(a => a.Login).IsRequired().HasMaxLength(64);
            modelBuilder.Entity<Author>().HasIndex(a => a.Login).IsUnique();

            modelBuilder.Entity<Tweet>().HasKey(t => t.Id);
            modelBuilder.Entity<Tweet>().Property(a => a.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Tweet>().Property(t => t.Content).IsRequired().HasMaxLength(2048);
            modelBuilder.Entity<Tweet>().Property(t => t.AuthorId).IsRequired();
            modelBuilder.Entity<Tweet>().Property(t => t.Created).IsRequired();
            modelBuilder.Entity<Tweet>().Property(t => t.Modified).IsRequired();
            modelBuilder.Entity<Tweet>().Property(t => t.Title).IsRequired().HasMaxLength(64);

            modelBuilder.Entity<Marker>().HasKey(m => m.Id);
            modelBuilder.Entity<Marker>().Property(m => m.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Marker>().Property(m => m.Name).IsRequired().HasMaxLength(32);
            modelBuilder.Entity<Marker>().HasIndex(m => m.Name).IsUnique();

            modelBuilder.Entity<Post>().HasKey(p => p.Id);
            modelBuilder.Entity<Post>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Post>().Property(p => p.Content).IsRequired().HasMaxLength(2048);
            modelBuilder.Entity<Post>().Property(p => p.TweetId).IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
