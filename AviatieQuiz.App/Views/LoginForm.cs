// -----------------------------------------------------------------------
// Autor: Macovei Razvan
// Data: 2025-05-27
// Descriere: LoginForm, e practic ecranul unde 
//            utilizatorul isi baga numele si parola ca sa intre 
//            in aplicatie. El ia datele astea si le trimite mai 
//            departe (la LoginPresenter) sa vada daca sunt bune. 
//            Daca userul greseste ceva, formularul afiseaza un 
//            mesaj de eroare. Daca totul e ok, atunci se inchide 
//            si lasa aplicatia sa mearga la ecranul principal.
// -----------------------------------------------------------------------

using AviatieQuiz.Core.Interfaces.Views;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AviatieQuiz.App.Views
{
    public partial class LoginForm : Form, ILoginView
    {
        public LoginForm()
        {
            try
            {
                InitializeComponent();
                if (this.btnLogin != null)
                {
                    this.btnLogin.Click += BtnLogin_Click;
                }
                this.AcceptButton = this.btnLogin;
               
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la initializarea LoginForm: {ex.Message}");
            }
        }

        public string Username => txtUsername.Text; // Presupunem ca avem un TextBox
        public string Password => txtPassword.Text; // La fel pentru parola

        public event EventHandler LoginAttempt;
        // public event EventHandler GoToRegisterClicked; 

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblErrorMessage != null) lblErrorMessage.Text = ""; // Presupunand un Label lblErrorMessage in designer
                LoginAttempt?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la click pe login: {ex.Message}");
            }
        }

        public void ShowErrorMessage(string message)
        {
            try
            {
                if (lblErrorMessage != null) // Presupunand un Label lblErrorMessage in designer
                {
                    lblErrorMessage.Text = message;
                    lblErrorMessage.ForeColor = Color.Red;
                    lblErrorMessage.Visible = true;
                }
                else
                {
                    MessageBox.Show(message, "Eroare Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la afisarea mesajului de eroare: {ex.Message}");
            }
        }

        public void NavigateToMainApplication()
        {
            try
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la navigarea catre aplicatia principala: {ex.Message}");
            }
        }

        public void ClearForm()
        {
            try
            {
                if (txtUsername != null) txtUsername.Text = "";
                if (txtPassword != null) txtPassword.Text = "";
                if (txtUsername != null) txtUsername.Focus();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la resetarea formularului: {ex.Message}");
            }
        }

        public void SetLoginButtonEnabled(bool enabled)
        {
            try
            {
                if (btnLogin != null)
                {
                    btnLogin.Enabled = enabled;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la activarea/dezactivarea butonului de login: {ex.Message}");
            }
        }
    }
}
