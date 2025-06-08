// -----------------------------------------------------------------------
// Autor: Macovei Razvan
// Data: 2025-05-27
// Descriere: 
// -----------------------------------------------------------------------


using System;

namespace AviatieQuiz.Core.Models
{
    public static class UserRoles
    {
        public const string Admin = "Admin";
        public const string StandardUser = "User";
    }

    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public User()
        {
            Id = Guid.NewGuid();
        }
    }
}
