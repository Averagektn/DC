using Microsoft.EntityFrameworkCore;
using REST.Storage.Common;

namespace REST.Storage.SqlDb
{
    public class PostgresDbContext : DbStorage
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=distcomp;Username=postgres;Password=postgres");
        }
    }
}
