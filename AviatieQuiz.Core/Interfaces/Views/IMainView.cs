// -----------------------------------------------------------------------
// Autor: Chiriac Mihai
// Data: 2025-05-27
// Descriere: 
// -----------------------------------------------------------------------



using AviatieQuiz.Core.Models;
using System;
using System.Collections.Generic;

namespace AviatieQuiz.Core.Interfaces.Views
{
    public interface IMainView
    {
        User CurrentUser { set; }
        string WelcomeMessage { set; }
        void DisplayDisciplines(List<string> disciplineNames);
        void NavigateToQuiz(QuizSession session);
        void NavigateToLogin();
        void SetAdminControlsVisibility(bool visible);
        event EventHandler ViewLoaded;
        event Action<string> DisciplineSelectedAndQuizStartRequested;
        event EventHandler LogoutClicked;
        event EventHandler AddQuestionsClicked;
        event EventHandler ReloadQuestionsClicked;
    }
}
