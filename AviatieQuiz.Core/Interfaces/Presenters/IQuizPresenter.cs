// -----------------------------------------------------------------------
// Autor: Macovei Razvan
// Data: 2025-05-27
// Descriere: 
// -----------------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AviatieQuiz.Core.Interfaces.Presenters
{
    public interface IQuizPresenter
    {
        void InitializeQuiz();
        void HandleNextQuestionOrFinish();
        void HandleSubmitAnswer();


    }
}