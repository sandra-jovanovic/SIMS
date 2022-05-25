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

        public List<User> GetAllNonBlockedUsers()
        {
            return userRepository.GetAllNonBlockedUsers();
        }

        public List<User> GetAllUsers()
        {
            return userRepository.GetAllUsers();
        }
    }
}
