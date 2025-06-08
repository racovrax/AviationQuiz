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
    public class AnswerOption
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public AnswerOption(int id, string text)
        {
            Id = id;
            Text = text;
        }
    }
}