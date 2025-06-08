// -----------------------------------------------------------------------
// Autor: Chiriac Mihai
// Data: 2025-05-27
// Descriere: Clasa MockUserDataService furnizeaza o implementare 
//            simulata (mock) a interfetei IUserDataService, destinata 
//            utilizarii in cadrul testelor unitare. Aceasta permite 
//            gestionarea unei liste interne de utilizatori in memorie, 
//            adaugarea de utilizatori de test si configurarea 
//            comportamentului metodei de autentificare. Prin simularea 
//            serviciului de date, clasa faciliteaza testarea izolata 
//            a componentelor care depind de IUserDataService, cum ar 
//            fi presenter-ii, fara a necesita interactiunea cu un 
//            sistem de stocare real (de exemplu, un fisier JSON).
// -----------------------------------------------------------------------

using AviatieQuiz.Core.Interfaces.Services;
using AviatieQuiz.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AviatieQuiz.App.Tests.Mocks
{
    public class MockUserDataService : IUserDataService
    {
        // O lista interna de utilizatori pentru testare
        private readonly List<User> _testUsers = new List<User>();
        public Func<string, string, User> AuthenticateUserFunc { get; set; } // Permite configurarea comportamentului

        public MockUserDataService()
        {
            // Comportament default pentru AuthenticateUserFunc
            AuthenticateUserFunc = (username, password) =>
            {
                return _testUsers.FirstOrDefault(u =>
                    u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) &&
                    u.Password == password); // Presupunand parole in clar, conform cerintei anterioare
            };
        }

        // Metoda helper pentru a adauga utilizatori de test
        public void AddTestUser(User user)
        {
            _testUsers.Add(user);
        }

        public void ClearTestUsers()
        {
            _testUsers.Clear();
        }

        // Implementarea metodelor din IUserDataService
        public bool AddUser(string username, string password, string role = UserRoles.StandardUser) // Am corectat User la UserRoles.StandardUser
        {
            if (_testUsers.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)))
            {
                return false; // Simuleaza esec daca user-ul exista
            }
            _testUsers.Add(new User { Username = username, Password = password, Role = role });
            return true;
        }

        public User GetUserByUsername(string username)
        {
            return _testUsers.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        public User AuthenticateUser(string username, string password)
        {
            // Foloseste functia configurabila sau comportamentul default
            return AuthenticateUserFunc?.Invoke(username, password);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _testUsers.AsReadOnly();
        }
    }
}
