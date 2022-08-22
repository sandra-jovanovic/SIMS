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

        public List<User> GetAllNonBlockedUsers()
        {
            var users = new List<User>();

            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var user = parseUserLine(line);
                    if (!user.Blocked) users.Add(user);
                }

            }

            return users;
        }

        public void Save(User user)
        {
            var users = GetAllUsers();

            foreach (var fileUser in users)
            {
                if (fileUser.JMBG == user.JMBG)
                    throw new ExistingIdException();

                if (fileUser.Email.Equals(user.Email))
                    throw new ExistingEmailException();
            }

            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                sw.WriteLine(user.ToString());
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

        public void BlockUser(User user)
        {
            var users = GetAllUsers();
            var modifiedUsers = new List<User>();

            users.ForEach(fileUser =>
            {
                if (fileUser.JMBG == user.JMBG) fileUser.Blocked = true;
                modifiedUsers.Add(fileUser);
            });

            using (StreamWriter sw = new StreamWriter(filePath, false))
            {
                sw.Write("");
            }

            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                modifiedUsers.ForEach(modifiedUser =>
                {
                    sw.WriteLine(modifiedUser);
                });
            }
        }

        public void UnblockUser(User user)
        {
            var users = GetAllUsers();
            var modifiedUsers = new List<User>();

            users.ForEach(fileUser =>
            {
                if (fileUser.JMBG == user.JMBG) fileUser.Blocked = false;
                modifiedUsers.Add(fileUser);
            });

            using (StreamWriter sw = new StreamWriter(filePath, false))
            {
                sw.Write("");
            }

            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                modifiedUsers.ForEach(modifiedUser =>
                {
                    sw.WriteLine(modifiedUser);
                });
            }
        }
    }
}
