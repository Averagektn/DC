namespace REST.Entity
{
    public class Marker
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<Tweet> Tweets { get; set; } = [];
    }
}
