// -----------------------------------------------------------------------
// Autor: Macovei Razvan
// Data: 2025-05-27
// Descriere: 
// -----------------------------------------------------------------------



using AviatieQuiz.Core.Models;
using System;
using System.Collections.Generic;

namespace AviatieQuiz.Core.Interfaces.Views
{
    public interface IQuizView
    {
        string WindowTitle { set; }
        string QuestionText { set; }
        string QuestionProgressText { set; }

        void DisplayQuestion(Question question, int currentQuestionNumber, int totalQuestions);
        void ShowFeedback(string feedbackMessage, bool isCorrect, int currentIncorrectCount, int maxIncorrectAllowed);
        void ClearFeedback();
        void ShowFinalScore(string scoreMessage, int correctAnswers, int totalQuestions, List<QuestionResult> results);
        void EnableNextButton(bool enable);
        void SetNextButtonText(string text);
        void ClearSelectedAnswer();
        void ResetView();
        void CloseView();
        void ShowError(string message);
        void SetAnswerOptionsEnabled(bool enabled);
        void UpdateLiveScoreDisplay(int correctAnswers, int incorrectAnswers, int totalAttemptedQuestions); // <<--- METODĂ NOUĂ

        void StartTimer(int totalSeconds);
        void StopTimer();
        void UpdateTimerDisplay(string timeString);

        event Action TimeExpired;
        event EventHandler ViewLoaded;
        event Action<int> AnswerOptionClicked;
        event EventHandler NextQuestionOrFinishClicked;
    }
}
