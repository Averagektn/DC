using Microsoft.EntityFrameworkCore;
using REST.Storage.Common;

namespace REST.Storage.SqlDb
{
    public class PostgresDbContext : DbStorage
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
    }
}
