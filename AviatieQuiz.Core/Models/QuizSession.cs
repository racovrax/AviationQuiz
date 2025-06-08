// -----------------------------------------------------------------------
// Autor: Agape Ioan
// Data: 2025-05-27
// Descriere: 
// -----------------------------------------------------------------------



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AviatieQuiz.Core.Models
{
    public class QuizSession
    {
        public string DisciplineName { get; private set; }
        private readonly List<Question> _questions;
        public List<QuestionResult> Results { get; private set; }
        public int CurrentQuestionIndex { get; private set; }

        public int TotalQuestions => _questions?.Count ?? 0;
        public bool IsQuizOver => CurrentQuestionIndex >= TotalQuestions;

        public int IncorrectAnswersCount { get; private set; }
        public int MaxIncorrectAnswers { get; private set; }

        public QuizSession(List<Question> questions, string disciplineName, int maxIncorrectAllowed = 4)
        {
            _questions = questions ?? new List<Question>();
            DisciplineName = disciplineName;
            Results = new List<QuestionResult>();
            CurrentQuestionIndex = 0;
            IncorrectAnswersCount = 0;
            MaxIncorrectAnswers = maxIncorrectAllowed;
        }

        public Question GetCurrentQuestion()
        {
            if (!IsQuizOver && _questions.Any() && CurrentQuestionIndex < _questions.Count)
            {
                return _questions[CurrentQuestionIndex];
            }
            return null;
        }

        public void AnswerCurrentQuestion(int selectedOptionId)
        {
            if (IsQuizOver) return;

            Question currentQuestion = GetCurrentQuestion();
            if (currentQuestion != null)
            {
                if (!Results.Any(r => r.OriginalQuestion.Id == currentQuestion.Id))
                {
                    var result = new QuestionResult(currentQuestion, selectedOptionId);
                    Results.Add(result);
                    if (!result.WasCorrect)
                    {
                        IncorrectAnswersCount++; 
                    }
                }
            }
        }
        public bool HasReachedMaxIncorrectAnswers => IncorrectAnswersCount >= MaxIncorrectAnswers;

        public bool GoToNextQuestion()
        {
            if (!IsQuizOver)
            {
                CurrentQuestionIndex++;
                return !IsQuizOver; 
            }
            return false; 
        }

        public string CalculateFinalScore()
        {
            if (!_questions.Any()) return "0/0";

            int correctAnswers = Results.Count(r => r.WasCorrect);
            return $"{correctAnswers}/{TotalQuestions}";
        }

        public void ResetSession()
        {
            Results.Clear();
            CurrentQuestionIndex = 0;
            IncorrectAnswersCount = 0;
        }
    }
}