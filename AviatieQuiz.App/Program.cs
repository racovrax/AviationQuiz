using AviatieQuiz.App.Presenters;
using AviatieQuiz.App.Views;
using AviatieQuiz.Core.Interfaces.Presenters;
using AviatieQuiz.Core.Interfaces.Services;
using AviatieQuiz.Core.Models;
using AviatieQuiz.Core.Services;
using System;
using System.Windows.Forms;

// -----------------------------------------------------------------------
// Autor: agape ioan
// Data: 2025-05-27
// Descriere: Clasa Program reprezinta punctul de intrare principal 
//            al aplicatiei AviatieQuiz.
// -----------------------------------------------------------------------


namespace AviatieQuiz.App
{
    internal static class Program
    {
        public static IUserDataService UserDataServiceInstance { get; private set; }

        public static IUserDataService GetUserDataService()
        {
            return UserDataServiceInstance;
        }

        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                UserDataServiceInstance = new UserDataService("users.json");
                User authenticatedUser = null;

                while (true)
                {
                    authenticatedUser = null;
                    using (LoginForm loginView = new LoginForm())
                    {
                        ILoginPresenter loginPresenter = new LoginPresenter(loginView, UserDataServiceInstance);

                        if (loginView.ShowDialog() == DialogResult.OK)
                        {
                            if (loginPresenter is LoginPresenter lp && lp.AuthenticatedUser != null)
                            {
                                authenticatedUser = lp.AuthenticatedUser;
                            }
                            else
                            {
                                authenticatedUser = UserDataServiceInstance.GetUserByUsername(loginView.Username);
                            }
                        }
                    }

                    if (authenticatedUser != null)
                    {
                        using (MainForm mainView = new MainForm())
                        {
                            IMainPresenter mainPresenter = new MainPresenter(mainView);
                            mainPresenter.Initialize(authenticatedUser);

                            DialogResult mainFormResult = mainView.ShowDialog();

                            if (mainPresenter is IDisposable disposableMainPresenter)
                            {
                                disposableMainPresenter.Dispose();
                            }

                            if (mainFormResult == DialogResult.Abort)
                            {
                                continue;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("A aparut o eroare neasteptata:\n" + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Application.Exit();
            }
        }
    }
}
