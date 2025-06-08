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
    public class QuestionResult
    {
        public Question OriginalQuestion { get; private set; }
        public int SelectedOptionId { get; private set; }
        public bool WasCorrect { get; private set; }

        public QuestionResult(Question originalQuestion, int selectedOptionId)
        {
            OriginalQuestion = originalQuestion;
            SelectedOptionId = selectedOptionId;
            WasCorrect = originalQuestion.IsCorrect(selectedOptionId);
        }

        public string QuestionText => OriginalQuestion?.Text;
        public int CorrectOptionIdFromQuestion => OriginalQuestion.CorrectOptionId;
    }
}
