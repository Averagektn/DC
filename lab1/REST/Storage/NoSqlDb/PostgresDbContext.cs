using Microsoft.EntityFrameworkCore;
using REST.Storage.Common;

namespace REST.Storage.NoSqlDb
{
    public class PostgresDbContext : DbStorage
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }
    }
}
