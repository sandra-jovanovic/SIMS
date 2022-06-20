using Apoteka.Models;
using System.Collections.Generic;

namespace Apoteka.Controllers
{
    public interface IUserController
    {
        List<User> GetAllUsers();
        List<User> GetAllNonBlockedUsers();
        void save(User user);
        void BlockUser(User user);
        void UnblockUser(User user);
    }
}
