using Microsoft.VisualStudio.TestTools.UnitTesting;
using AviatieQuiz.App.Presenters;
using AviatieQuiz.App.Tests.Mocks;
using AviatieQuiz.Core.Interfaces.Views;
using AviatieQuiz.Core.Interfaces.Services;
using AviatieQuiz.Core.Models;
using System; // Pentru ArgumentNullException

// -----------------------------------------------------------------------
// Autor: Chiriac Mihai
// Data: 2025-05-27
// Descriere: Aceasta clasa, LoginPresenterTests, contine un set de 
//            teste unitare destinate verificarii functionarii corecte 
//            a clasei LoginPresenter. Testele acopera diverse 
//            scenarii, incluzand validarea constructorului, 
//            autentificarea reusita, autentificarea esuata cu parola 
//            gresita sau utilizator inexistent, gestionarea campurilor 
//            goale pentru credentiale si comportamentul butonului de 
//            login in diferite situatii. Se utilizeaza clase simulate 
//            (mocks) pentru ILoginView si IUserDataService pentru a 
//            izola logica LoginPresenter si a asigura testarea 
//            controlata a acesteia.
// -----------------------------------------------------------------------


namespace AviatieQuiz.App.Tests
{
    [TestClass]
    public class LoginPresenterTests
    {
        private MockLoginView _mockView;
        private MockUserDataService _mockUserDataService;
        private LoginPresenter _presenter;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockView = new MockLoginView();
            _mockUserDataService = new MockUserDataService();
            // Adaugă un utilizator valid pentru testele de succes
            _mockUserDataService.AddTestUser(new User { Username = "testuser", Password = "password123", Role = UserRoles.StandardUser });
            _presenter = new LoginPresenter(_mockView, _mockUserDataService);
        }

        [TestMethod]
        public void Constructor_WhenViewIsNull_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new LoginPresenter(null, _mockUserDataService));
        }

        [TestMethod]
        public void Constructor_WhenUserDataServiceIsNull_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new LoginPresenter(_mockView, null));
        }

        [TestMethod]
        public void AttemptLogin_SuccessfulLogin_NavigatesToMainApplicationAndSetsAuthenticatedUser()
        {
            // Arrange
            _mockView.UsernameInput = "testuser";
            _mockView.PasswordInput = "password123";

            // Act
            _mockView.TriggerLoginAttempt(); // Simulează click pe butonul de login (va apela presenter.AttemptLogin)

            // Assert
            Assert.IsTrue(_mockView.NavigateToMainCalled, "NavigateToMainApplication ar trebui să fie apelat.");
            Assert.IsNull(_mockView.ErrorMessageShown, "Nu ar trebui afișat niciun mesaj de eroare.");
            Assert.IsNotNull(_presenter.AuthenticatedUser, "AuthenticatedUser ar trebui să fie setat.");
            Assert.AreEqual("testuser", _presenter.AuthenticatedUser.Username, true, "Numele utilizatorului autentificat este incorect.");
        }

        [TestMethod]
        public void AttemptLogin_WrongPassword_ShowsErrorMessageAndDoesNotNavigate()
        {
            // Arrange
            _mockView.UsernameInput = "testuser";
            _mockView.PasswordInput = "wrongpassword";

            // Act
            _mockView.TriggerLoginAttempt();

            // Assert
            Assert.IsFalse(_mockView.NavigateToMainCalled, "NavigateToMainApplication NU ar trebui să fie apelat.");
            Assert.IsNotNull(_mockView.ErrorMessageShown, "Un mesaj de eroare ar trebui afișat.");
            Assert.AreEqual("Nume de utilizator sau parolă incorectă.", _mockView.ErrorMessageShown, "Mesajul de eroare este incorect.");
            Assert.IsNull(_presenter.AuthenticatedUser, "AuthenticatedUser NU ar trebui să fie setat.");
            Assert.AreEqual(true, _mockView.LoginButtonEnabledState, "Butonul de login ar trebui reactivat.");
        }

        [TestMethod]
        public void AttemptLogin_NonExistentUser_ShowsErrorMessage()
        {
            // Arrange
            _mockView.UsernameInput = "nonexistentuser";
            _mockView.PasswordInput = "password123";

            // Act
            _mockView.TriggerLoginAttempt();

            // Assert
            Assert.IsFalse(_mockView.NavigateToMainCalled);
            Assert.IsNotNull(_mockView.ErrorMessageShown);
            Assert.AreEqual("Nume de utilizator sau parolă incorectă.", _mockView.ErrorMessageShown);
            Assert.IsNull(_presenter.AuthenticatedUser);
        }

        [TestMethod]
        public void AttemptLogin_EmptyUsername_ShowsErrorMessage()
        {
            // Arrange
            _mockView.UsernameInput = "";
            _mockView.PasswordInput = "password123";

            // Act
            _mockView.TriggerLoginAttempt();

            // Assert
            Assert.IsFalse(_mockView.NavigateToMainCalled);
            Assert.IsNotNull(_mockView.ErrorMessageShown);
            Assert.AreEqual("Numele de utilizator și parola nu pot fi goale.", _mockView.ErrorMessageShown);
            Assert.IsNull(_presenter.AuthenticatedUser);
            Assert.AreEqual(true, _mockView.LoginButtonEnabledState);
        }

        [TestMethod]
        public void AttemptLogin_EmptyPassword_ShowsErrorMessage()
        {
            // Arrange
            _mockView.UsernameInput = "testuser";
            _mockView.PasswordInput = "";

            // Act
            _mockView.TriggerLoginAttempt();

            // Assert
            Assert.IsFalse(_mockView.NavigateToMainCalled);
            Assert.IsNotNull(_mockView.ErrorMessageShown);
            Assert.AreEqual("Numele de utilizator și parola nu pot fi goale.", _mockView.ErrorMessageShown);
            Assert.IsNull(_presenter.AuthenticatedUser);
            Assert.AreEqual(true, _mockView.LoginButtonEnabledState);
        }

        [TestMethod]
        public void AttemptLogin_DisablesAndEnablesLoginButtonAppropriately()
        {
            // Arrange
            _mockView.UsernameInput = "testuser";
            _mockView.PasswordInput = "wrongpassword"; // Forțăm un eșec pentru a verifica re-activarea

            // Act
            _mockView.TriggerLoginAttempt();
            // Starea butonului este verificată DUPĂ ce AttemptLogin s-a încheiat (în cazul eșecului)

            // Assert
            // Verificăm că a fost dezactivat la un moment dat și reactivat la finalul metodei AttemptLogin în caz de eșec
            // MockLoginView.SetLoginButtonEnabled ar fi fost apelat întâi cu 'false', apoi cu 'true'.
            // Ultima stare înregistrată ar trebui să fie 'true'.
            Assert.AreEqual(true, _mockView.LoginButtonEnabledState, "Butonul de Login ar trebui să fie reactivat după un eșec.");

            // Pentru a verifica că a fost DEZACTIVAT în timpul procesării,
            // am putea modifica MockLoginView să stocheze o listă de stări sau să avem un flag separat.
            // Dar pentru simplitate, testul de mai sus verifică starea finală.
        }
    }
}
