using REST.Entity.Common;

namespace REST.Entity.Db
{
    public class Marker(string name) : AbstractEntity
    {
        public string Name { get; set; } = name;

        public Marker() : this(string.Empty) { }
        public Marker(int id, string name) : this(name)
        {
            Id = id;
        }
    }
}
