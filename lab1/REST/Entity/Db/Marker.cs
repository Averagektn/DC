namespace REST.Entity.Db
{
    public class Marker(string name) : AbstractEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = name;
        public ICollection<Tweet> Tweets { get; set; } = [];

        public Marker() : this(string.Empty) { }
        public Marker(int id, string name, ICollection<Tweet> tweets) : this(name)
        {
            Id = id;
            Tweets = tweets;
        }
    }
}
