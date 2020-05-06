using MongoDB.Driver;
using UserDomain.Entities;
using UserDomain.Repositories;
using UserInfra.Context;

namespace UserInfra.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly IUserDataContext<User> _dataContext;

        public UserRepository(IUserDataContext<User> dataContext)
        {
            _dataContext = dataContext;
        }

        public bool UserExists(string email)
        {
            var user = GetByEmail(email);
            return !(user is null);
        }

        public void CreateUser(User user)
        {
            _dataContext.DataCollection.InsertOne(user);
        }
        
        public User GetByEmail(string email)
        {
            return _dataContext.DataCollection.Find(user => user.Email == email).SingleOrDefault();
        }
        
    }
}