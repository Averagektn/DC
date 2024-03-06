﻿using REST.Entity.Common;
using System.ComponentModel.DataAnnotations;

namespace REST.Entity.Db
{
    public class Tweet(string title, string content, DateTime created, DateTime modified) : AbstractEntity
    {
        public int AuthorId { get; set; }
        [MinLength(2)]
        public string Title { get; set; } = title;
        [MinLength(2)]
        public string Content { get; set; } = content;
        public DateTime Created { get; set; } = created;
        public DateTime Modified { get; set; } = modified;
        public ICollection<Post> Posts { get; set; } = [];
        public ICollection<Marker> Markers { get; set; } = [];
        public Author Author { get; set; } = null!;

        public Tweet() : this(string.Empty, string.Empty, DateTime.Now, DateTime.Now) { }
        public Tweet(int id, int authorId, string title, string content, DateTime created, DateTime modified) 
            : this(title, content, created, modified)
        {
            Id = id;
            AuthorId = authorId;
        }
    }
}
