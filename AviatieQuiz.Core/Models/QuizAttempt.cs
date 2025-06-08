
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
    public class QuizAttempt
    {
        public string DisciplineName { get; set; }
        public string Score { get; set; }
        public DateTime DateAttempted { get; set; }
        public int TotalQuestionsInAttempt { get; set; }
        public int CorrectAnswersInAttempt { get; set; }

        public QuizAttempt(string disciplineName, string score, DateTime dateAttempted, int totalQuestions, int correctAnswers)
        {
            DisciplineName = disciplineName;
            Score = score;
            DateAttempted = dateAttempted;
            TotalQuestionsInAttempt = totalQuestions;
            CorrectAnswersInAttempt = correctAnswers;
        }
        public QuizAttempt() { }
    }
}
