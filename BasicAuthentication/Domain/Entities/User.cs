using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BasicAuthentication.Domain.Entities
{
    public class User
    {
        public User()
        {

        }

        public User(string email, string firstName, string lastName, string password)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Password = EncriptyPassword(password);
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("firstName")]
        public string FirstName { get; set; }

        [BsonElement("lastName")]
        public string LastName { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }

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
    }
}