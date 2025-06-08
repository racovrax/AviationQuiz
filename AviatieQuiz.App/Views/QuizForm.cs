using AviatieQuiz.Core.Interfaces.Views;
using AviatieQuiz.Core.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

// -----------------------------------------------------------------------
// Autor: Caraulan Egor
// Data: 2025-05-27
// Descriere: QuizForm, e ecranul unde se intampla 
//            testul efectiv. El afiseaza intrebarile, variantele de 
//            raspuns ca butoane, cronometrul si scorul. Cand userul 
//            apasa pe un raspuns sau pe 'Next', formularul ii zice 
//            la QuizPresenter ce s-a intamplat. Tot el se ocupa sa 
//            arate daca raspunsul e corect sau gresit si la final 
//            scorul total.
// -----------------------------------------------------------------------


namespace AviatieQuiz.App.Views
{
    public partial class QuizForm : Form, IQuizView
    {
        private List<Button> _answerOptionButtons;
        private int _remainingSecondsForQuiz;

        public QuizForm()
        {
            try
            {
                InitializeComponent();
                _answerOptionButtons = new List<Button>();
                this.Load += QuizForm_Load;
                if (this.btnNextQuestion != null)
                {
                    this.btnNextQuestion.Click += BtnNextQuestion_Click;
                }
                if (this.timer1 != null)
                {
                    this.timer1.Interval = 1000;
                    this.timer1.Tick += QuizCountdownTimer_Tick;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la initializarea QuizForm: {ex.Message}");
                throw;
            }
        }

        public string WindowTitle { set { this.Text = value; } }
        public string QuestionText { set { if (lblQuestionText != null) lblQuestionText.Text = value; } }
        public string QuestionProgressText { set { if (lblQuestionProgress != null) lblQuestionProgress.Text = value; } }

        public void DisplayQuestion(Question question, int currentQuestionNumber, int totalQuestions)
        {
            if (question == null) return;
            this.QuestionText = question.Text;
            this.QuestionProgressText = $"Intrebarea {currentQuestionNumber} / {totalQuestions}";
            if (gbOptions != null) gbOptions.Controls.Clear();
            _answerOptionButtons.Clear();
            int topPosition = 10;
            int buttonHeight = 40;
            int verticalSpacing = 10;
            if (question.Options != null)
            {
                foreach (var option in question.Options)
                {
                    Button answerButton = new Button
                    {
                        Text = option.Text,
                        Tag = option.Id,
                        AutoSize = false,
                        Height = buttonHeight,
                        Width = (gbOptions != null ? gbOptions.ClientSize.Width - 20 : 200),
                        Location = new Point(10, topPosition),
                        TextAlign = ContentAlignment.MiddleCenter,
                    };
                    answerButton.Click += DynamicAnswerButton_Click;
                    _answerOptionButtons.Add(answerButton);
                    if (gbOptions != null) gbOptions.Controls.Add(answerButton);
                    topPosition += buttonHeight + verticalSpacing;
                }
            }
            SetAnswerOptionsEnabled(true);
        }


        public void ShowFeedback(string feedbackMessage, bool isCorrect, int currentIncorrectCount, int maxIncorrectAllowed)
        {
            if (lblFeedback != null)
            {
                lblFeedback.Text = feedbackMessage;
                lblFeedback.ForeColor = isCorrect ? Color.DarkGreen : Color.DarkRed;
                lblFeedback.Visible = true;
            }
        }
        public void ClearFeedback() { if (lblFeedback != null) { lblFeedback.Visible = false; lblFeedback.Text = string.Empty; } }
        public void ShowError(string message) { MessageBox.Show(this, message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        public void SetAnswerOptionsEnabled(bool enabled) { foreach (var btn in _answerOptionButtons) { btn.Enabled = enabled; } }
        public void EnableNextButton(bool enable) { if (btnNextQuestion != null) btnNextQuestion.Enabled = enable; }
        public void ClearSelectedAnswer() { }
        public void CloseView() { this.Close(); }
        public void ShowFinalScore(string scoreMessage, int correctAnswers, int totalQuestions, List<QuestionResult> results)
        {
            // Afiseaza scorul final si ascunde optiunile
            try
            {
                if (results != null) { }
                MessageBox.Show(this, $"{scoreMessage}", "Rezultate Test", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (gbOptions != null) gbOptions.Visible = false;
                if (lblQuestionText != null) lblQuestionText.Text = "Test finalizat! Poti inchide aceasta fereastra.";
                if (lblQuestionProgress != null) lblQuestionProgress.Visible = false;
                if (lblFeedback != null) lblFeedback.Visible = false;
                if (btnNextQuestion != null) { btnNextQuestion.Text = "Inchide"; btnNextQuestion.Enabled = true; }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la afisarea scorului final: {ex.Message}");
                throw;
            }
        }
        public void ResetView()
        {
            try
            {
                if (gbOptions != null) { gbOptions.Controls.Clear(); gbOptions.Visible = true; }
                _answerOptionButtons.Clear();
                if (lblQuestionText != null) lblQuestionText.Text = "Se incarca intrebarea...";
                if (lblQuestionProgress != null) { lblQuestionProgress.Text = ""; lblQuestionProgress.Visible = true; }
                ClearFeedback();
                EnableNextButton(false);
                if (btnNextQuestion != null) btnNextQuestion.Visible = true;
                SetAnswerOptionsEnabled(true);
                StopTimer(); 
                if (lblTimerDisplay != null) lblTimerDisplay.Text = "30:00"; // Valoare inițială pentru afișaj
                if (lblLiveScoreDisplay != null) lblLiveScoreDisplay.Text = "Corecte: 0 | Greșite: 0"; // Resetează scorul live
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la resetarea view-ului: {ex.Message}");
                throw;
            }
        }

        public event Action TimeExpired;

        public void StartTimer(int totalSeconds)
        {
            _remainingSecondsForQuiz = totalSeconds;
            UpdateTimerDisplayVisuals();
            var timerControl = this.timer1 ?? this.timer1; // Folosește timer-ul corect
            if (timerControl != null)
            {
                timerControl.Enabled = true;
                timerControl.Start();
            }
        }

        public void StopTimer()
        {
            if (this.timer1 != null)
            {
                this.timer1.Enabled = false;
            }
        }

        public void UpdateTimerDisplay(string timeString)
        {
            if (lblTimerDisplay != null)
            {
                lblTimerDisplay.Text = timeString;
            }
        }

        private void UpdateTimerDisplayVisuals()
        {
            TimeSpan time = TimeSpan.FromSeconds(_remainingSecondsForQuiz);
            string timeString = string.Format("{0:D2}:{1:D2}", time.Minutes, time.Seconds);

            if (lblTimerDisplay != null)
            {
                // Asigură actualizarea pe thread-ul UI dacă e necesar (deși Tick e pe UI)
                if (lblTimerDisplay.InvokeRequired)
                {
                    lblTimerDisplay.Invoke(new Action(() => lblTimerDisplay.Text = timeString));
                }
                else
                {
                    lblTimerDisplay.Text = timeString;
                }
            }
        }

        private void QuizCountdownTimer_Tick(object sender, EventArgs e)
        {
            if (_remainingSecondsForQuiz > 0)
            {
                _remainingSecondsForQuiz--;
                UpdateTimerDisplayVisuals();
            }
            else
            {
                // Verifică dacă timer-ul este încă activ pentru a evita multiple invocări
                var timerControl = this.timer1;
                if (timerControl != null && timerControl.Enabled)
                {
                    StopTimer();
                    TimeExpired?.Invoke();
                }
            }
        }

        public void UpdateLiveScoreDisplay(int correctAnswers, int incorrectAnswers, int totalAttemptedQuestions)
        {
            if (lblLiveScoreDisplay != null)
            {
                lblLiveScoreDisplay.Text = $"Corecte: {correctAnswers} | Greșite: {incorrectAnswers} (Răspunsuri: {totalAttemptedQuestions})";
            }
        }

        public event EventHandler ViewLoaded;
        public event Action<int> AnswerOptionClicked;
        public event EventHandler NextQuestionOrFinishClicked;

        private void QuizForm_Load(object sender, EventArgs e)
        {
            try
            {
                ViewLoaded?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la incarcarea QuizForm: {ex.Message}");
            }
        }

        private void DynamicAnswerButton_Click(object sender, EventArgs e)
        {
            try
            {
                Button clickedButton = sender as Button;
                if (clickedButton != null && clickedButton.Tag is int)
                {
                    int selectedOptionId = (int)clickedButton.Tag;
                    AnswerOptionClicked?.Invoke(selectedOptionId);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la selectarea raspunsului: {ex.Message}");
            }
        }

        private void BtnNextQuestion_Click(object sender, EventArgs e)
        {
            // Butonul Next gestioneaza trecerea la urmatoarea intrebare sau inchiderea testului
            try
            {
                if (btnNextQuestion != null && btnNextQuestion.Text == "Inchide")
                {
                    CloseView();
                }
                else
                {
                    NextQuestionOrFinishClicked?.Invoke(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la click pe butonul Next: {ex.Message}");
            }
        }
        public void SetNextButtonText(string text)
        {
            if (btnNextQuestion != null)
            {
                btnNextQuestion.Text = text;
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            if (timer1 != null)
            {
                timer1.Stop();
                timer1.Dispose();
            }
        }

        private void lblTimerDisplay_Click(object sender, EventArgs e)
        {

        }
    }
}
