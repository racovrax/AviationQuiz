// -----------------------------------------------------------------------
// Autor: Macovei Razvan
// Data: 2025-05-27
// Descriere: Clasa LoginPresenter se ocupa de logica pentru login. 
//            Ia ce scrie utilizatorul in campurile de username si parola, 
//            verifica daca sunt bune (daca exista userul in "baza de date" - la noi fisierul JSON)
//            si apoi ii spune la formular (la view) daca e ok sa mearga mai departe 
//            sau daca trebuie sa afiseze o eroare.
// -----------------------------------------------------------------------

using AviatieQuiz.Core.Interfaces.Views;
using AviatieQuiz.Core.Interfaces.Presenters;
using AviatieQuiz.Core.Interfaces.Services;
using AviatieQuiz.Core.Models;
using System;

namespace AviatieQuiz.App.Presenters
{
    public class LoginPresenter : ILoginPresenter
    {
        private readonly ILoginView _view;
        private readonly IUserDataService _userDataService;
        public User AuthenticatedUser { get; private set; }

        public LoginPresenter(ILoginView view, IUserDataService userDataService)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _userDataService = userDataService ?? throw new ArgumentNullException(nameof(userDataService));

            _view.LoginAttempt += OnLoginAttempt;
        }

        private void OnLoginAttempt(object sender, EventArgs e)
        {
            AttemptLogin();
        }

        public void AttemptLogin()
        {
            string username = _view.Username;
            string password = _view.Password;

            _view.SetLoginButtonEnabled(false); // Blochez butonul cat timp verific

            // Verific daca a scris ceva in campuri
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                _view.ShowErrorMessage("Numele de utilizator si parola nu pot fi goale.");
                _view.SetLoginButtonEnabled(true); // Deblochez butonul
                return;
            }

            try
            {
                User userFromDb = _userDataService.AuthenticateUser(username, password);

                if (userFromDb != null)
                {
                    // E bun, useru exista si parola e corecta
                    AuthenticatedUser = userFromDb;
                    _view.NavigateToMainApplication(); // Mergem la ecranul principal
                }
                else
                {
                    // User sau parola gresita
                    AuthenticatedUser = null;
                    _view.ShowErrorMessage("Nume de utilizator sau parola incorecta.");
                    _view.SetLoginButtonEnabled(true); // Deblochez butonul
                }
            }
            catch (Exception ex)
            {
                // A aparut o eroare
                Console.WriteLine($"Eroare la autentificare: {ex.Message}");
                _view.ShowErrorMessage("A aparut o eroare neasteptata la autentificare.");
                _view.SetLoginButtonEnabled(true); // Deblochez butonul
            }
        }

        public void Dispose()
        {
            // Aici ne dezabonam de la eveniment ca sa nu avem probleme cu memoria
            if (_view != null)
            {
                _view.LoginAttempt -= OnLoginAttempt;
            }
        }
    }
}
