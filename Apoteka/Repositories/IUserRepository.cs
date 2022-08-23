using Apoteka.Models;
using System.Collections.Generic;

namespace Apoteka.Repositories
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        void AddNewUser(User user);
        void UpdateUser(User user);
    }
}
