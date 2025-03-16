using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Userhash
{
    public static class UserManager
    {
        private static string filePath = "users.xml";
        private static List<User> users = new List<User>();

        public static void LoadUsers()
        {
            if (File.Exists(filePath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<User>), new Type[] { typeof(Admin) });
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    users = (List<User>)serializer.Deserialize(fs);
                }
            }
            else
            {
                users.Add(new Admin("admin", "admin"));
                SaveUsers();
            }
        }

        public static void SaveUsers()
        {
            Console.WriteLine("Ukládám users.xml...");
            XmlSerializer serializer = new XmlSerializer(typeof(List<User>), new Type[] { typeof(Admin) });
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                serializer.Serialize(fs, users);
            }
            Console.WriteLine("users.xml byl uložen.");
        }

        public static User Authenticate(string username, string password)
        {
            foreach (var user in users)
            {
                if (user.Username == username && user.VerifyPassword(password))
                {
                    return user;
                }
            }
            return null;
        }

        public static void AddUser(User user)
        {
            users.Add(user);
            SaveUsers();
        }

        public static List<User> GetUsers()
        {
            return users;
        }
    }
}