using AviatieQuiz.Core.Models;
using System.Collections.Generic;

// -----------------------------------------------------------------------
// Autor: Macovei Razvan
// Data: 2025-05-27
// Descriere:   IUserDataService
// -----------------------------------------------------------------------


namespace AviatieQuiz.Core.Interfaces.Services
{
    public interface IUserDataService
    {
        bool AddUser(string username, string password, string role = UserRoles.StandardUser);

        User GetUserByUsername(string username);

        User AuthenticateUser(string username, string password);

        IEnumerable<User> GetAllUsers();
    }
}
