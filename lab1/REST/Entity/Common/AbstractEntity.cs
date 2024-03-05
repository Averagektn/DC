using Microsoft.EntityFrameworkCore;

namespace REST.Entity.Common
{
    public abstract class AbstractEntity
    {
        public int Id { get; set; }
    }
}
