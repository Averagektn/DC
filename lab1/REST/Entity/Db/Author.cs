using REST.Entity.Common;

namespace REST.Entity.Db
{
    public class Author(string login, string password, string firstName, string lastName) : AbstractEntity
    {
        public string Login { get; set; } = login;
        public string Password { get; set; } = password;
        public string FirstName { get; set; } = firstName;
        public string LastName { get; set; } = lastName;

        public Author(int id, string login, string password, string firstName, string lastName) :
            this(login, password, firstName, lastName)
        {
            Id = id;
        }

        public Author() : this(string.Empty, string.Empty, string.Empty, string.Empty) { }
    }
}
