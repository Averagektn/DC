namespace REST.Entity.Db
{
    public class Author(string login, string password, string firstName, string lastName) : AbstractEntity
    {
        public int Id { get; set; }
        public string Login { get; set; } = login;
        public string Password { get; set; } = password;
        public string FirstName { get; set; } = firstName;
        public string LastName { get; set; } = lastName;
        public ICollection<Tweet> Tweets { get; set; } = [];

        public Author(int id, string login, string password, string firstName, string lastName, ICollection<Tweet> tweets) :
            this(login, password, firstName, lastName)
        {
            Id = id;
            Tweets = tweets;
        }

        public Author() : this(string.Empty, string.Empty, string.Empty, string.Empty) { }
    }
}
