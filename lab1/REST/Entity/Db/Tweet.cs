using REST.Entity.Common;

namespace REST.Entity.Db
{
    public class Tweet(string title, string content, DateTime created, DateTime modified) : AbstractEntity
    {
        public int AuthorId { get; set; }
        public string Title { get; set; } = title;
        public string Content { get; set; } = content;
        public DateTime Created { get; set; } = created;
        public DateTime Modified { get; set; } = modified;

        public Tweet() : this(string.Empty, string.Empty, DateTime.Now, DateTime.Now) { }
        public Tweet(int id, int authorId, string title, string content, DateTime created, DateTime modified) 
            : this(title, content, created, modified)
        {
            Id = id;
            AuthorId = authorId;
        }
    }
}
