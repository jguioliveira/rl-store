using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UserManagement.Domain.Entities
{
    public class User
    {
        public User(string email, string firstName, string lastName, string password, bool active)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Password = EncriptyPassword(password);
            Active = active;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }

        [BsonElement("email")]
        public string Email { get; private set; }

        [BsonElement("firstName")]
        public string FirstName { get; private set; }

        [BsonElement("lastName")]
        public string LastName { get; private set; }

        [BsonElement("password")]
        public string Password { get; private set; }

        [BsonElement("active")]
        public bool Active { get; private set; }

        [BsonElement("groups")]
        private Collection<string> groups;

        public IReadOnlyCollection<string> Groups { get { return groups; } }

        public bool CheckPassword(string password)
        {
            PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
            return passwordHasher.VerifyHashedPassword(this, Password, password) != PasswordVerificationResult.Failed;           
        }

        private string EncriptyPassword(string password)
        {
            PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
            return passwordHasher.HashPassword(this, password);
        }

        public void AddGroup(string group)
        {
            if(groups is null)
            {
                groups = new Collection<string>();
            }

            groups.Add(group);
        }

        public void AddGroupRange(ICollection<string> groups)
        {
            if (groups is null)
            {
                groups = new Collection<string>();
            }

            foreach (var item in groups)
            {
                this.groups.Add(item);
            }
        }
    }
}