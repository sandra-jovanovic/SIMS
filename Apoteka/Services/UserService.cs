using Apoteka.Models;
using Apoteka.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apoteka.Services
{
    public class UserService : IUserService
    {
        private IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public void BlockUser(User user)
        {
            userRepository.BlockUser(user);
        }

        public List<User> GetAllNonBlockedUsers()
        {
            return userRepository.GetAllNonBlockedUsers();
        }

        public List<User> GetAllUsers()
        {
            return userRepository.GetAllUsers();
        }

        public void save(User user)
        {
            userRepository.save(user);
        }

        public void UnblockUser(User user)
        {
            userRepository.UnblockUser(user);
        }
    }
}
