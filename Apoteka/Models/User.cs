namespace Apoteka.Models
{
    public enum UserRole {
        Farmaceut,
        Upravnik,
        Lekar
    }

    public class User
    {
        public User()
        {
        }

        public User(string jMBG, string email, string password, string name, string surname, string phone, UserRole role, bool blocked)
        {
            JMBG = jMBG;
            Email = email;
            Password = password;
            Name = name;
            Surname = surname;
            Phone = phone;
            Role = role;
            Blocked = blocked;
        }

        public string JMBG { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public UserRole Role { get; set; }

        public bool Blocked { get; set; }

        public override string ToString()
        {
            return $"{JMBG},{Email},{Password},{Name},{Surname},{Phone},{Role},{Blocked}";
        }
    }
}
