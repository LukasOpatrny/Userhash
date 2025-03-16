using System;
using System.Security.Cryptography;
using System.Text;

namespace Userhash
{
    public class User
    {
        public string Username { get; set; }
        public string HashedPassword { get; set; }

        public User() { }

        public User(string username, string password)
        {
            Username = username;
            HashedPassword = HashPassword(password);
        }

        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }

        public bool VerifyPassword(string password)
        {
            return HashedPassword == HashPassword(password);
        }
    }

    public class Admin : User
    {
        public Admin() { }

        public Admin(string username, string password) : base(username, password) { }
    }
}