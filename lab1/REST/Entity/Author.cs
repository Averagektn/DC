namespace REST.Entity
{
    public class Author
    {
        public int Id {  get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public ICollection<Tweet> Tweets { get; set; } = [];
    }
}
