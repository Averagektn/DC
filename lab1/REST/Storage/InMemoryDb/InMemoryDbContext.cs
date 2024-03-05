using Microsoft.EntityFrameworkCore;
using REST.Storage.Common;

namespace REST.Storage.InMemoryDb
{
    public class InMemoryDbContext : DbStorage
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("MyInMemDb");
        }
    }
}
