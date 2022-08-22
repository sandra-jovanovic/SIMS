using Apoteka.Models;
using Apoteka.Services;
using System.Collections.Generic;

namespace Apoteka.Controllers
{
    public class UserController
    {
        private IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        public List<User> GetAllUsers()
        {
            return userService.GetAllUsers();
        }

        public void Save(User user)
        {
            userService.Save(user);
        }

        public User AuthenticateUser(string username, string password)
        {
            return userService.AuthenticateUser(username, password);
        }

        public void BlockUser(User user)
        {
            userService.BlockUser(user);
        }

        public void UnblockUser(User user)
        {
            userService.UnblockUser(user);
        }
    }
}
