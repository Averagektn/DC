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

        public DbStorage() 
        { 
            Database.EnsureCreated();
            Database.EnsureDeleted();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("Authors");

                entity.HasKey(e => e.Id);

                entity.Property(a => a.Id).ValueGeneratedOnAdd();
                entity.Property(a => a.Password).IsRequired().HasMaxLength(128);
                entity.Property(a => a.FirstName).IsRequired().HasMaxLength(64).IsUnicode();
                entity.Property(a => a.LastName).IsRequired().HasMaxLength(64).IsUnicode();
                entity.Property(a => a.Login).IsRequired().HasMaxLength(64).IsUnicode();

                entity.HasIndex(a => a.Login).IsUnique();
                entity.HasAlternateKey(a => a.Login);
            });

            modelBuilder.Entity<Post>((entity) =>
            {
                entity.ToTable("posts");

                entity.HasKey(p => p.Id);

                entity.Property(p => p.Id).ValueGeneratedOnAdd();
                entity.Property(p => p.Content).IsRequired().HasMaxLength(2048);
                entity.Property(p => p.TweetId).IsRequired();
            });

            modelBuilder.Entity<Tweet>(entity =>
            {
                entity.ToTable("tweets");

                entity.HasKey(t => t.Id);

                entity.Property(a => a.Id).ValueGeneratedOnAdd();
                entity.Property(t => t.Content).IsRequired().HasMaxLength(2048);
                entity.Property(t => t.AuthorId).IsRequired();
                entity.Property(t => t.Created).IsRequired();
                entity.Property(t => t.Modified).IsRequired();
                entity.Property(t => t.Title).IsRequired().HasMaxLength(64);

                entity.HasIndex(t => t.Title).IsUnique();
                entity.HasAlternateKey(t => t.Title);
            });

            modelBuilder.Entity<Marker>(entity =>
            {
                entity.ToTable("markers");

                entity.HasKey(m => m.Id);
                entity.Property(m => m.Id).ValueGeneratedOnAdd();
                entity.Property(m => m.Name).IsRequired().HasMaxLength(32);

                entity.HasIndex(m => m.Name).IsUnique();
                entity.HasAlternateKey(m => m.Name);
            });
        }
    }
}
