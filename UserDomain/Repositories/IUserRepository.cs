using UserDomain.Entities;

namespace UserDomain.Repositories
{
    public interface IUserRepository
    {
        bool UserExists(string email);
        void CreateUser(User user);
        User GetByEmail(string email);
    }
}