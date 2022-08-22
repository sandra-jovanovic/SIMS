using Apoteka.Models;
using System.Collections.Generic;

namespace Apoteka.Services
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        List<User> GetAllNonBlockedUsers();
        void Save(User user);
        void BlockUser(User user);
        void UnblockUser(User user);
        User AuthenticateUser(string username, string password);
    }
}
