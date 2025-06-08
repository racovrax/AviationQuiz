using AviatieQuiz.Core.Interfaces.Views;
using AviatieQuiz.Core.Interfaces.Presenters;
using AviatieQuiz.Core.Models;
using AviatieQuiz.Core;
using System;
using System.Collections.Generic;
using System.Linq;

// -----------------------------------------------------------------------
// Autor: Chiriac Mihai
// Data: 2025-05-27
// Descriere: MainPresenter, e "sefa" pe ecranul principal 
//            care apare dupa ce te loghezi. Ea se prinde cand se 
//            incarca formularul, ia disciplinele din fisierul JSON 
//            cu intrebari si le spune la view sa le afiseze. 
//            Tot ea se ocupa si cand userul alege o disciplina si 
//            vrea sa dea test, pregatind tot ce trebuie pentru quiz. 
//            Mai stie sa faca si logout si are logica pentru ce ar 
//            trebui sa faca un admin
// -----------------------------------------------------------------------

namespace AviatieQuiz.App.Presenters
{
    public class MainPresenter : IMainPresenter
    {
        private readonly IMainView _view;
        private User _currentUser;
        private List<Discipline> _availableDisciplines;
        private readonly string _quizDataFilePath;

        public MainPresenter(IMainView view, string quizDataFilePath = "quiz.json")
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _quizDataFilePath = quizDataFilePath;

            _view.ViewLoaded += OnViewLoaded;
            _view.DisciplineSelectedAndQuizStartRequested += OnDisciplineSelectedAndQuizStartRequested;
            _view.LogoutClicked += OnLogoutClicked;
            _view.AddQuestionsClicked += OnAddQuestionsClicked;
        }

        public void Initialize(User authenticatedUser)
        {
            _currentUser = authenticatedUser ?? throw new ArgumentNullException(nameof(authenticatedUser));
            _view.CurrentUser = _currentUser;
            _view.WelcomeMessage = $"Bine ai venit, {_currentUser.Username}! (Rol: {_currentUser.Role})";
            _view.SetAdminControlsVisibility(_currentUser.Role == UserRoles.Admin);
        }

        private void OnViewLoaded(object sender, EventArgs e)
        {
            try
            {
                LoadDisciplines();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la încărcarea disciplinelor: {ex.Message}");
                _view.DisplayDisciplines(new List<string>());
            }
        }

        private void LoadDisciplines()
        {
            _availableDisciplines = DataLoader.LoadDisciplinesAndQuestions(_quizDataFilePath);
            if (_availableDisciplines != null && _availableDisciplines.Any())
            {
                _view.DisplayDisciplines(_availableDisciplines.Select(d => d.Name).ToList());
            }
            else
            {
                _view.DisplayDisciplines(new List<string>());
            }
        }

        private void OnDisciplineSelectedAndQuizStartRequested(string disciplineName)
        {
            try
            {
                StartQuizForDiscipline(disciplineName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la pornirea quizului: {ex.Message}");
            }
        }

        public void StartQuizForDiscipline(string disciplineName)
        {
            if (string.IsNullOrWhiteSpace(disciplineName))
            {
                return;
            }

            Discipline selectedDiscipline = _availableDisciplines?.FirstOrDefault(d => d.Name.Equals(disciplineName, StringComparison.OrdinalIgnoreCase));

            if (selectedDiscipline == null)
            {
                return;
            }

            if (selectedDiscipline.Questions == null || !selectedDiscipline.Questions.Any())
            {
                return;
            }

            List<Question> questionsForQuiz = selectedDiscipline.GetRandomQuestions(selectedDiscipline.Questions.Count).ToList();

            QuizSession quizSession = new QuizSession(questionsForQuiz, selectedDiscipline.Name);
            _view.NavigateToQuiz(quizSession);
        }

        private void OnLogoutClicked(object sender, EventArgs e)
        {
            try
            {
                Logout();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la delogare: {ex.Message}");
            }
        }

        public void Logout()
        {
            _currentUser = null;
            _view.NavigateToLogin();
        }

        private void OnAddQuestionsClicked(object sender, EventArgs e)
        {
            try
            {
                RequestAddQuestionsView();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la deschiderea formularului de adăugare întrebări: {ex.Message}");
            }
        }

        public void RequestAddQuestionsView()
        {
            if (_currentUser?.Role == UserRoles.Admin)
            {
            }
            else
            {
            }
        }

        public void Dispose()
        {
            if (_view != null)
            {
                _view.ViewLoaded -= OnViewLoaded;
                _view.DisciplineSelectedAndQuizStartRequested -= OnDisciplineSelectedAndQuizStartRequested;
                _view.LogoutClicked -= OnLogoutClicked;
                _view.AddQuestionsClicked -= OnAddQuestionsClicked;
            }
        }
    }
}
