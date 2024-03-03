namespace REST.Entity
{
    public class Tweet
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public ICollection<Post> Posts { get; set; } = [];
        public ICollection<Marker> Markers { get; set; } = [];
    }
}
