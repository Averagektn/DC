namespace REST.Entity
{
    public class Post
    {
        public int Id { get; set; } 
        public int TweetId { get; set; }
        public string Content { get; set; } = null!;
    }
}
