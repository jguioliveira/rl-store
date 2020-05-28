using System.Collections.Generic;
using System.Linq;
using UserManagement.Domain.Security;

namespace UserManagement.Domain.Entities
{
    public class User
    {
        private List<string> _groups;

        public User(string email, string firstName, string lastName, string password, bool active)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Password = EncriptyPassword(password);
            Active = active;
        }

        public string Id { get; private set; }

        public string Email { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string Password { get; private set; }

        public bool Active { get; private set; }

        public IReadOnlyCollection<string> Groups { get { return _groups.AsReadOnly(); }  set { _groups = value?.ToList() ?? new List<string>(); } }

        public bool CheckPassword(string password)
        {
            return PasswordHasher.VerifyHashedPassword(Password, password); ;
        }

        private string EncriptyPassword(string password)
        {
            return PasswordHasher.HashPassword(password);
        }

        public void AddGroup(string group)
        {
            if(_groups is null)
            {
                _groups = new List<string>();
            }

            _groups.Add(group);
        }

        public void AddGroupRange(IEnumerable<string> groups)
        {
            if (_groups is null)
            {
                _groups = new List<string>();
            }

            _groups.AddRange(groups);
        }

        
    }
}