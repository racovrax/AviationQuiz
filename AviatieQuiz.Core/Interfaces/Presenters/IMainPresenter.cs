// -----------------------------------------------------------------------
// Autor: Chiriac Mihai
// Data: 2025-05-27
// Descriere:
// -----------------------------------------------------------------------



using AviatieQuiz.Core.Models;

namespace AviatieQuiz.Core.Interfaces.Presenters
{
    public interface IMainPresenter
    {
        // initializare a presenter-ului cu utilizatorul autentificat
        void Initialize(User authenticatedUser);
        void StartQuizForDiscipline(string disciplineName);
        void Logout();
        void RequestAddQuestionsView();
    }
}
