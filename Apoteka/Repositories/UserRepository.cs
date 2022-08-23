using Apoteka.Exceptions;
using Apoteka.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace Apoteka.Repositories
{
    public class UserRepository : IUserRepository
    {
        private const string filePath = "./users.txt";
        public List<User> GetAllUsers()
        {
            if (!File.Exists(filePath))
            {
                using (File.Create(filePath)) ;
            }

            var users = new List<User>();

            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var user = parseUserLine(line);
                    users.Add(user);
                }

            }

            return users;
        }

        public void AddNewUser(User user)
        {
            var users = GetAllUsers();

            foreach (var fileUser in users)
            {
                if (fileUser.JMBG == user.JMBG)
                    throw new ExistingIdException();

                if (fileUser.Email.Equals(user.Email))
                    throw new ExistingEmailException();
            }

            users.Add(user);

            SaveUsers(users);
        }

        public void UpdateUser(User user)
        {
            var users = GetAllUsers();
            foreach (User u in users)
            {
                if (u.JMBG.Equals(user.JMBG))
                {
                    u.Name = user.Name;
                    u.Surname = user.Surname;
                    u.Role = user.Role;
                    u.Phone = user.Phone;
                    u.Blocked = user.Blocked;
                }
            }

            SaveUsers(users);
        }

        private void SaveUsers(List<User> users)
        {

            using (StreamWriter sw = new StreamWriter(filePath, false))
            {
                sw.Write("");
            }

            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                users.ForEach(modifiedUser =>
                {
                    sw.WriteLine(modifiedUser);
                });
            }

        }

        private User parseUserLine(string line)
        {
            var fields = line.Split(",");
            var JMBG = fields[0];
            var email = fields[1];
            var password = fields[2];
            var name = fields[3];
            var surname = fields[4];
            var phone = fields[5];
            var role = fields[6];
            var blocked = fields[7];

            return new User(JMBG, email, password, name, surname, phone, (UserRole)Enum.Parse(typeof(UserRole), role), bool.Parse(blocked));
        }

    }
}
