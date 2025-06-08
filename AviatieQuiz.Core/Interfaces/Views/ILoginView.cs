// -----------------------------------------------------------------------
// Autor: Macovei Razvan
// Data: 2025-05-27
// Descriere:
// -----------------------------------------------------------------------


using System;

namespace AviatieQuiz.Core.Interfaces.Views
{
    public interface ILoginView
    {
        string Username { get; }
        string Password { get; }
        void ShowErrorMessage(string message);
        void NavigateToMainApplication();
        void ClearForm();
        void SetLoginButtonEnabled(bool enabled);
        event EventHandler LoginAttempt;
    }
}
