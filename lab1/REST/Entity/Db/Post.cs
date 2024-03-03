namespace REST.Entity.Db
{
    public class Post(string content) : AbstractEntity
    {
        public int Id { get; set; }
        public int TweetId { get; set; }
        public string Content { get; set; } = content;

        public Post() : this(string.Empty) { }
        public Post(int id, int tweetId, string content) : this(content)
        {
            Id = id;
            TweetId = tweetId;
        }
    }
}
