using System.Linq;
using HelloWorld.Models;
using System.Collections.Generic;


namespace HelloWorld
{
    public interface IUserRepository
    {
        User LogIn(string email, string password);
        //UserModel Register(string email, string password);
    }

    public class UserRepository : IUserRepository
    {
        public IEnumerable<User> Users
        {
            get
            {
                var items = new[]
                {
                    new User{ Id=100, Email="admin@test.com", Password="password"},
                    new User{ Id=101, Email="mike@test.com", Password="password"},
                    new User{ Id=102, Email="dave@test.com", Password="password"},
                    new User{ Id=103, Email="lisa@test.com", Password="password"},
                };
                return items;
            }
        }

        public User LogIn(string email, string password)
        {
            return Users.SingleOrDefault(t => t.Email.ToLower() == email.ToLower()
                                        && t.Password == password);
        }

        //public UserModel Register(string email, string password)
        //{
        //    var user = DatabaseAccessor.Instance.User
        //            .Add(new 
        //            {
        //                UserEmail = email,
        //                UserHashedPassword = password
        //            });
        //    
        //    DatabaseAccessor.Instance.SaveChanges();
        //    
        //    return new UserModel { Id = user.Entity.UserId, Name = user.Entity.UserEmail };
        //    return new UserModel { Id = 103, Email = "lisa@test.com", Password = "password" };
        //}
    }
}