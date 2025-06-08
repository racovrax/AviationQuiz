// -----------------------------------------------------------------------
// Autor: Chiriac Mihai
// Data: 2025-05-27
// Descriere: Aceasta clasa, MockLoginView, reprezinta o implementare 
//            simulata (mock) a interfetei ILoginView. Este utilizata 
//            in cadrul testelor unitare pentru a izola LoginPresenter 
//            de implementarea concreta a formularului de login. 
//            Clasa permite setarea datelor de intrare (utilizator, 
//            parola) si verificarea actiunilor pe care presenter-ul 
//            le-ar invoca asupra view-ului (afisarea erorilor, 
//            navigarea, starea butoanelor), facilitand astfel 
//            testarea logicii presenter-ului in mod controlat.
// -----------------------------------------------------------------------

using AviatieQuiz.Core.Interfaces.Views;
using System;
using System.Collections.Generic; // Neutilizat direct, dar poate fi pastrat pentru consistenta

namespace AviatieQuiz.App.Tests.Mocks
{
    public class MockLoginView : ILoginView
    {
        // Proprietati pentru a simula input-ul utilizatorului
        public string UsernameInput { get; set; }
        public string PasswordInput { get; set; }

        // Proprietati pentru a verifica ce a facut presenter-ul cu view-ul
        public string ErrorMessageShown { get; private set; }
        public bool NavigateToMainCalled { get; private set; }
        public bool FormCleared { get; private set; }
        public bool? LoginButtonEnabledState { get; private set; }

        // Implementarea proprietatilor din ILoginView
        public string Username => UsernameInput;
        public string Password => PasswordInput;

        // Implementarea evenimentului din ILoginView
        public event EventHandler LoginAttempt;

        // Metoda pentru a simula click-ul pe butonul de login
        public void TriggerLoginAttempt()
        {
            LoginAttempt?.Invoke(this, EventArgs.Empty);
        }

        // Implementarea metodelor din ILoginView
        public void ShowErrorMessage(string message)
        {
            ErrorMessageShown = message;
        }

        public void NavigateToMainApplication()
        {
            NavigateToMainCalled = true;
        }

        public void ClearForm()
        {
            FormCleared = true;
            // Optional, reseteaza si inputurile simulate
            // UsernameInput = string.Empty;
            // PasswordInput = string.Empty;
        }

        public void SetLoginButtonEnabled(bool enabled)
        {
            LoginButtonEnabledState = enabled;
        }

        // Metoda helper pentru a reseta starea mock-ului intre teste
        public void Reset()
        {
            UsernameInput = null;
            PasswordInput = null;
            ErrorMessageShown = null;
            NavigateToMainCalled = false;
            FormCleared = false;
            LoginButtonEnabledState = null;
        }
    }
}
