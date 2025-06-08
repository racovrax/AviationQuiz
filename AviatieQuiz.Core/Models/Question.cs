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
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public List<AnswerOption> Options { get; set; }
        public int CorrectOptionId { get; set; }
        public Question(int id, string text, List<AnswerOption> options, int correctOptionId)
        {
            Id = id;
            Text = text;
            Options = options;
            CorrectOptionId = correctOptionId;
        }

        public bool IsCorrect(int selectedOptionId)
        {
            return selectedOptionId == CorrectOptionId;
        }
    }
}