using AviatieQuiz.Core.Interfaces.Services;
using AviatieQuiz.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

// -----------------------------------------------------------------------
// Autor: Macovei Razvan
// Data: 2025-05-27
// Descriere: Clasa UserDataService este responsabilă pentru gestionarea
//            datelor utilizatorilor în aplicația AviatieQuiz. 
//            
// -----------------------------------------------------------------------




namespace AviatieQuiz.Core.Services { 

    public class UserDataService : IUserDataService
    {
        private readonly string _usersFilePath;
        private List<User> _users;

        private const string DefaultAdminUsername = "admin";
        private const string DefaultAdminPassword = "admin";

        public UserDataService(string usersFileName = "users.json")
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            _usersFilePath = Path.Combine(baseDirectory, usersFileName);

            LoadUsers();
            EnsureDefaultAdminExists();
        }

        private void LoadUsers()
        {
            if (File.Exists(_usersFilePath))
            {
                try
                {
                    string jsonString = File.ReadAllText(_usersFilePath);
                    _users = JsonSerializer.Deserialize<List<User>>(jsonString) ?? new List<User>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Eroare la încărcarea utilizatorilor: {ex.Message}");
                    _users = new List<User>();
                }
            }
            else
            {
                _users = new List<User>();
            }
        }

        private void SaveUsers()
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(_users, options);
                File.WriteAllText(_usersFilePath, jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la salvarea utilizatorilor: {ex.Message}");
            }
        }

        private void EnsureDefaultAdminExists()
        {
            if (!_users.Any(u => u.Username.Equals(DefaultAdminUsername, StringComparison.OrdinalIgnoreCase)))
            {
                User adminUser = new User
                {
                    Username = DefaultAdminUsername,
                    Password = DefaultAdminPassword,
                    Role = UserRoles.Admin
                };
                _users.Add(adminUser);
                SaveUsers();
            }
            else
            {
                User admin = _users.FirstOrDefault(u => u.Username.Equals(DefaultAdminUsername, StringComparison.OrdinalIgnoreCase));
                if (admin != null)
                {
                    bool changed = false;
                    if (admin.Role != UserRoles.Admin)
                    {
                        admin.Role = UserRoles.Admin;
                        changed = true;
                    }
                    if (changed) SaveUsers();
                }
            }
        }

        public bool AddUser(string username, string password, string role = UserRoles.StandardUser)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                {
                    return false;
                }

                if (_users.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)))
                {
                    return false;
                }

                User newUser = new User
                {
                    Username = username,
                    Password = password,
                    Role = role
                };

                _users.Add(newUser);
                SaveUsers();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la adaugarea utilizatorului: {ex.Message}");
                return false;
            }
        }

        public User GetUserByUsername(string username)
        {
            try
            {
                return _users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la cautarea utilizatorului: {ex.Message}");
                return null;
            }
        }

        public User AuthenticateUser(string username, string password)
        {
            try
            {
                User user = GetUserByUsername(username);
                if (user != null && user.Password == password)
                {
                    return user;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la autentificarea utilizatorului: {ex.Message}");
                return null;
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _users.AsReadOnly();
        }
    }
}
