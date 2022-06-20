using Apoteka.Models;
using Apoteka.Repositories;
using Apoteka.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apoteka.Controllers
{
    public class UserController : IUserController
    {
        private IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        public void BlockUser(User user)
        {
            userService.BlockUser(user);
        }

        public List<User> GetAllNonBlockedUsers()
        {
            return userService.GetAllNonBlockedUsers();
        }

        public List<User> GetAllUsers()
        {
            return userService.GetAllUsers();
        }

        public void save(User user)
        {
            userService.save(user);
        }

        public void UnblockUser(User user)
        {
            userService.UnblockUser(user);
        }
    }
}
