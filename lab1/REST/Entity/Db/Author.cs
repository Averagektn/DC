﻿using REST.Entity.Common;
using System.ComponentModel.DataAnnotations;

namespace REST.Entity.Db
{
    public class Author(string login, string password, string firstName, string lastName) : AbstractEntity
    {
        [MinLength(2)]
        public string Login { get; set; } = login;
        [MinLength(2)]
        public string Password { get; set; } = password;
        [MinLength(2)]
        public string FirstName { get; set; } = firstName;
        [MinLength(2)]
        public string LastName { get; set; } = lastName;
        public ICollection<Tweet> Tweets { get; set; } = [];


        public Author(int id, string login, string password, string firstName, string lastName) :
            this(login, password, firstName, lastName)
        {
            Id = id;
        }

        public Author() : this(string.Empty, string.Empty, string.Empty, string.Empty) { }
    }
}
