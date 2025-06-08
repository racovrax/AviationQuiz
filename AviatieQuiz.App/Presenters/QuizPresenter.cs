using AviatieQuiz.Core.Interfaces.Views;
using AviatieQuiz.Core.Interfaces.Presenters;
using AviatieQuiz.Core.Models;
using System;
using System.Linq;

// -----------------------------------------------------------------------
// Autor: Caraulan Egor
// Data: 2025-05-27
// Descriere: Clasa QuizPresenter e creierul din spatele testului 
//            propriu-zis. Cand incepe testul, ea se ocupa sa ia 
//            intrebarile din QuizSession si sa le trimita la formular 
//            (la IQuizView) sa le afiseze. Tot ea tine minte ce 
//            intrebare e curenta, proceseaza raspunsurile date de 
//            utilizator, actualizeaza scorul (cate corecte, cate gresite),
//            verifica daca s-a atins limita de greseli sau daca a 
//            expirat timpul. Practic, gestioneaza tot fluxul testului, 
//            de la prima intrebare pana la afisarea rezultatului final.
// -----------------------------------------------------------------------


namespace AviatieQuiz.App.Presenters
{
    public class QuizPresenter : IQuizPresenter
    {
        private readonly IQuizView _view;
        private readonly QuizSession _quizSession;
        private Question _currentQuestion;
        private bool _quizFinishedFlag = false; // Flag pentru a gestiona starea de finalizare

        public QuizPresenter(IQuizView view, QuizSession quizSession)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _quizSession = quizSession ?? throw new ArgumentNullException(nameof(quizSession));

            _view.ViewLoaded += OnViewLoaded;
            _view.AnswerOptionClicked += OnAnswerOptionClicked;
            _view.NextQuestionOrFinishClicked += OnNextQuestionOrFinishClicked;
            _view.TimeExpired += OnTimeExpired;
        }

        private void OnViewLoaded(object sender, EventArgs e)
        {
            InitializeQuiz();
        }

        private void UpdateLiveScore()
        {
            int correct = _quizSession.Results.Count(r => r.WasCorrect);
            // incorrectAnswersCount este deja actualizat în QuizSession și reprezintă totalul greșelilor din sesiune
            int totalAttempted = _quizSession.Results.Count;
            _view.UpdateLiveScoreDisplay(correct, _quizSession.IncorrectAnswersCount, totalAttempted);
        }

        private void OnAnswerOptionClicked(int selectedOptionId)
        {
            if (_quizFinishedFlag || _currentQuestion == null) return;

            _quizSession.AnswerCurrentQuestion(selectedOptionId);
            UpdateLiveScore(); // Actualizează scorul live după răspuns

            bool isCorrect = _currentQuestion.IsCorrect(selectedOptionId);
            string correctAnswerText = _currentQuestion.Options.FirstOrDefault(opt => opt.Id == _currentQuestion.CorrectOptionId)?.Text ?? "N/A";
            string mainFeedback = isCorrect ? "Corect!" : $"Greșit. Răspunsul corect era: {correctAnswerText}";
            string fullFeedbackMessage = $"{mainFeedback}\nGreșeli: {_quizSession.IncorrectAnswersCount} / {_quizSession.MaxIncorrectAnswers}";

            _view.ShowFeedback(fullFeedbackMessage, isCorrect, _quizSession.IncorrectAnswersCount, _quizSession.MaxIncorrectAnswers);
            _view.SetAnswerOptionsEnabled(false);
            _view.EnableNextButton(true);

            if (_quizSession.HasReachedMaxIncorrectAnswers)
            {
                FinishQuiz($"Ai atins limita de {_quizSession.MaxIncorrectAnswers} greșeli!");
                return;
            }

            if (_quizSession.CurrentQuestionIndex + 1 >= _quizSession.TotalQuestions)
            {
                _view.SetNextButtonText("Finalizează Testul");
            }
            else
            {
                _view.SetNextButtonText("Următoarea Întrebare");
            }
        }

        private void OnNextQuestionOrFinishClicked(object sender, EventArgs e)
        {
            if (_quizFinishedFlag) return;
            HandleNextQuestionOrFinish();
        }

        private void OnTimeExpired()
        {
            if (_quizFinishedFlag) return;
            FinishQuiz("Timpul a expirat!");
        }

        public void InitializeQuiz()
        {
            _quizFinishedFlag = false;
            if (_quizSession == null || _quizSession.TotalQuestions == 0)
            {
                _view.ShowError("Sesiunea de quiz nu este validă sau nu conține întrebări.");
                _view.SetAnswerOptionsEnabled(false);
                _view.EnableNextButton(false);
                _view.StopTimer();
                _view.UpdateLiveScoreDisplay(0, 0, 0); // Scor inițial
                return;
            }
            _view.WindowTitle = $"Quiz: {_quizSession.DisciplineName}";
            _quizSession.ResetSession(); // Resetează și IncorrectAnswersCount
            _view.ResetView();
            UpdateLiveScore(); // Afișează scorul inițial (0/0)
            LoadNextQuestion();
            _view.StartTimer(30 * 60); // 30 minute
        }

        public void HandleSubmitAnswer() { /* Logica e în OnAnswerOptionClicked */ }

        public void HandleNextQuestionOrFinish()
        {
            if (_quizFinishedFlag) return;
            _view.ClearFeedback();
            bool movedToNext = _quizSession.GoToNextQuestion();

            if (movedToNext)
            {
                LoadNextQuestion();
            }
            else
            {
                FinishQuiz();
            }
        }

        private void LoadNextQuestion()
        {
            if (_quizFinishedFlag) return;
            _currentQuestion = _quizSession.GetCurrentQuestion();
            if (_currentQuestion != null)
            {
                _view.DisplayQuestion(_currentQuestion, _quizSession.CurrentQuestionIndex + 1, _quizSession.TotalQuestions);
                _view.EnableNextButton(false);
            }
            else
            {
                if (!_quizFinishedFlag) FinishQuiz();
            }
        }

        private void FinishQuiz(string specialEndMessage = null)
        {
            if (_quizFinishedFlag) return; // Previne apeluri multiple
            _quizFinishedFlag = true;

            _view.StopTimer();
            _view.SetAnswerOptionsEnabled(false);
            _view.EnableNextButton(true);
            _view.SetNextButtonText("Test Terminat");

            UpdateLiveScore(); // Asigură-te că scorul final e afișat și în live display

            int correctAnswers = _quizSession.Results.Count(r => r.WasCorrect);
            string finalScoreString = _quizSession.CalculateFinalScore();
            string displayMessage = !string.IsNullOrEmpty(specialEndMessage) ?
                                    $"{specialEndMessage}\nScorul final: {finalScoreString}" :
                                    $"Test Finalizat! Scorul tău: {finalScoreString}";

            _view.ShowFinalScore(displayMessage, correctAnswers, _quizSession.TotalQuestions, _quizSession.Results);
        }

        public void Dispose()
        {
            if (_view != null)
            {
                _view.ViewLoaded -= OnViewLoaded;
                _view.AnswerOptionClicked -= OnAnswerOptionClicked;
                _view.NextQuestionOrFinishClicked -= OnNextQuestionOrFinishClicked;
                _view.TimeExpired -= OnTimeExpired;
            }
        }
    }
}
