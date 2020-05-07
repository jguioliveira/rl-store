using System;

namespace UserDomain.Entities
{
    public class User
    {

        public User(string email, string firstName, string lastName, string password)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
        }

        public string Email { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string Password { get; private set; }

        public bool CheckPassword(string password)
        {
            return password == Password;
        }

    }
}