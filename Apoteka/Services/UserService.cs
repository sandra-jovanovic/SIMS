using Apoteka.Models;
using Apoteka.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Apoteka.Services
{
    public class UserService : IUserService
    {
        private IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public User AuthenticateUser(string email, string password)
        {
            var users = userRepository.GetAllUsers().FindAll(user => !user.Blocked);
            var user = users.FirstOrDefault(user => user.Email == email && user.Password == password);

            return user;
        }

        public void BlockUser(User user)
        {
            user.Blocked = true;
            userRepository.UpdateUser(user);
        }

        public List<User> GetAllNonBlockedUsers()
        {
            return userRepository.GetAllUsers().FindAll(user => !user.Blocked);
        }

        public List<User> GetAllUsers()
        {
            return userRepository.GetAllUsers();
        }

        public void Save(User user)
        {
            userRepository.AddNewUser(user);
        }

        public void UnblockUser(User user)
        {
            user.Blocked = false;
            userRepository.UpdateUser(user);
        }
    }
}
