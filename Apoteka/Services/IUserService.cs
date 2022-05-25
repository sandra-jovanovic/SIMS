using Apoteka.Models;
using System.Collections.Generic;

namespace Apoteka.Services
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        List<User> GetAllNonBlockedUsers();
    }
}
