using Microsoft.EntityFrameworkCore;
using REST.Entity.Db;

namespace REST.Storage
{
    public abstract class DbStorage : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Marker> Markers { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tweet> Tweets { get; set; }
    }
}
