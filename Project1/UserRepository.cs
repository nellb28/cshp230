using System.Linq;
using HelloWorld.Models;
using System.Collections.Generic;

namespace HelloWorld
{
    public interface IUserRepository
    {
        _DELETEUser LogIn(string email, string password);
    }

    public class UserRepository : IUserRepository
    {
        public IEnumerable<_DELETEUser> Users
        {
            get
            {
                var items = new[]
                {
                    new _DELETEUser{ Id=100, Email="admin", Password="admin", IsAdmin=true},
                    new _DELETEUser{ Id=101, Email="mike", Password="mike"},
                    new _DELETEUser{ Id=102, Email="dave", Password="dave"},
                    new _DELETEUser{ Id=103, Email="lisa", Password="lisa"},
                };
                return items;
            }
        }

        public _DELETEUser LogIn(string email, string password)
        {
            return Users.SingleOrDefault(t => t.Email.ToLower() == email.ToLower()
                                        && t.Password == password);
        }
    }
}