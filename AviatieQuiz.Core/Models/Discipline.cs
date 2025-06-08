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
    public class Discipline
    {
        public string Name { get; set; }
        public List<Question> Questions { get; set; }

        public Discipline(string name)
        {
            Name = name;
            Questions = new List<Question>();
        }

        public void AddQuestion(Question question)
        {
            Questions.Add(question);
        }

        public List<Question> GetRandomQuestions(int count)
        {
            if (Questions == null || !Questions.Any())
                return new List<Question>();

            var random = new Random();
            return Questions.OrderBy(q => random.Next()).Take(Math.Min(count, Questions.Count)).ToList();
        }

        public List<Question> GetAllQuestions()
        {
            return Questions ?? new List<Question>();
        }
    }
}