using AviatieQuiz.Core.Interfaces.Presenters;
using AviatieQuiz.Core.Interfaces.Views;
using AviatieQuiz.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

// -----------------------------------------------------------------------
// Autor: Chiriac Mihai
// Data: 2025-05-27
// Descriere: Acesta este formularul principal, MainForm, care apare dupa 
//            ce te-ai logat. De aici poti alege ce test vrei sa dai 
//            din lista aia de discipline, poti incepe testul, te poti 
//            deloga sau, daca esti admin, ai niste butoane in plus 
//            (cum ar fi ala de reincarcat intrebarile). Tot de aici 
//            poti deschide si fisierul de ajutor daca apesi pe butonul 
//            respectiv.
// -----------------------------------------------------------------------

namespace AviatieQuiz.App.Views
{
    public partial class MainForm : Form, IMainView
    {
        private const string HelpFileName = "help.chm";
        public MainForm()
        {
            try
            {
                InitializeComponent();
                if (this.btnStartQuiz != null)
                    this.btnStartQuiz.Click += BtnStartQuiz_Click;
                if (this.btnLogout != null)
                    this.btnLogout.Click += BtnLogout_Click;
                if (this.btnReloadQuestions != null) 
                {
                    this.btnReloadQuestions.Click += BtnReloadQuestions_Click;
                    
                }
                if (this.button1 != null) // Asigură-te că ai acest buton în designer
                {
                    this.button1.Click += button1_Click;
                }
                this.Load += MainForm_Load;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la inițializarea MainForm: {ex.Message}");
            }
        }

        private User _currentUser;
        public User CurrentUser
        {
            set
            {
                try
                {
                    _currentUser = value;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Eroare la setarea utilizatorului curent: {ex.Message}");
                }
            }
        }

        public string WelcomeMessage
        {
            set { try { if (lblWelcomeMessage != null) lblWelcomeMessage.Text = value; } catch (Exception ex) { Console.WriteLine($"Eroare la setarea mesajului de bun venit: {ex.Message}"); } }
        }

        public event EventHandler ViewLoaded;
        public event Action<string> DisciplineSelectedAndQuizStartRequested;
        public event EventHandler LogoutClicked;
        public event EventHandler AddQuestionsClicked;

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                ViewLoaded?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la încărcarea MainForm: {ex.Message}");
            }
        }

        private void BtnStartQuiz_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstDisciplines.SelectedItem != null)
                {
                    string selectedDiscipline = lstDisciplines.SelectedItem.ToString();
                    DisciplineSelectedAndQuizStartRequested?.Invoke(selectedDiscipline);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la pornirea quizului: {ex.Message}");
            }
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            try
            {
                LogoutClicked?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la delogare: {ex.Message}");
            }
        }

        private void BtnAddQuestions_Click(object sender, EventArgs e)
        {
            try
            {
                AddQuestionsClicked?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la adăugarea întrebărilor: {ex.Message}");
            }
        }

        public void DisplayDisciplines(List<string> disciplineNames)
        {
            try
            {
                if (lstDisciplines != null)
                {
                    lstDisciplines.DataSource = null;
                    lstDisciplines.DataSource = disciplineNames;
                    lstDisciplines.ClearSelected();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la afișarea disciplinelor: {ex.Message}");
            }
        }

        public void NavigateToQuiz(QuizSession session)
        {
            try
            {
                this.Hide();
                using (QuizForm quizView = new QuizForm())
                {
                    AviatieQuiz.Core.Interfaces.Presenters.IQuizPresenter quizPresenter =
                        new AviatieQuiz.App.Presenters.QuizPresenter(quizView, session);
                    quizView.ShowDialog(this);
                }
                this.Show();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la navigarea către quiz: {ex.Message}");
            }
        }

        public void NavigateToLogin()
        {
            try
            {
                this.DialogResult = DialogResult.Abort;
                this.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la revenirea la login: {ex.Message}");
            }
        }
        public event EventHandler ReloadQuestionsClicked;

        private void BtnReloadQuestions_Click(object sender,EventArgs e)
        {
            ReloadQuestionsClicked?.Invoke(this, EventArgs.Empty);
            
        }

        public void SetAdminControlsVisibility(bool visible)
        {
            try
            {
                if (btnReloadQuestions != null)
                {
                    btnReloadQuestions.Visible = visible;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la setarea vizibilității controalelor admin: {ex.Message}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Construiește calea completă către fișierul CHM
                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                string helpFilePath = Path.Combine(basePath, HelpFileName);

                if (File.Exists(helpFilePath))
                {
                    // Afișează fișierul de ajutor
                    Help.ShowHelp(this, helpFilePath);
                }
                else
                {
                    MessageBox.Show($"Fișierul de ajutor '{HelpFileName}' nu a fost găsit la calea: {helpFilePath}",
                                    "Eroare Ajutor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"A apărut o eroare la deschiderea fișierului de ajutor: {ex.Message}",
                                "Eroare Ajutor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddQuestions_Click_1(object sender, EventArgs e)
        {

        }

        private void btnReloadQuestions_Click(object sender, EventArgs e)
        {

        }
    }
}
