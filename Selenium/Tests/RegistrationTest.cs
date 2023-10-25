using NUnit.Framework;
using Selenium.Framework.Factories;
using Selenium.Framework.Pages;
using System.Configuration;
using System.IO;
using System.Linq;

namespace Selenium.Framework.Tests
{
    public class RegistrationTests : BaseTest
    {
        private User user;
        private User newUser;
        private Header header;
        private LoginPage loginPage;
        private MyApplicationsPage myApplicationsPage;

        [SetUp]
        protected void Initialize()
        {
            user = User.GetDefaultUser();
            header = new Header(Driver);
            loginPage = new LoginPage(Driver);
            myApplicationsPage = new MyApplicationsPage(Driver);
        }

        [Test]
        public void UserRegistrationTest()
        {
            newUser = UserFactory.GenerateRegularUser();
            RegistrationPage registrationPage = SiteNavigator.NavigateToRegistrationPage(Driver, user);
            HomePage homePage = registrationPage.RegisterUser(newUser);
            Logger.Info("Registrating a new user");
            Assert.True(homePage.OnHeader().GetWelcomeText.Contains(newUser.FirstName));
        }

        [Test]
        public void UserRegistrationLoginTest()
        {
            newUser = UserFactory.GenerateRegularUser();
            RegistrationPage registrationPage = SiteNavigator.NavigateToRegistrationPage(Driver, user);
            HomePage homePage = registrationPage.RegisterUser(newUser);
            Logger.Info("New user registered");
            homePage.OnHeader().LogOutLink.Click();
            Logger.Info("User logged out");
            loginPage.Login(newUser);
            Assert.True(homePage.OnHeader().GetWelcomeText.Contains(newUser.FirstName));
        }

        [Test]
        public void DeveloperRegistrationTest()
        {
            newUser = UserFactory.GenerateDeveloperUser();
            RegistrationPage registrationPage = SiteNavigator.NavigateToRegistrationPage(Driver, user);
            HomePage homePage = registrationPage.RegisterUser(newUser);
            Logger.Info("New Developer registered");
            homePage.OnHeader().MyApplicationsLink.Click();
            myApplicationsPage.AssertCanOpenMyApplicationPage();
        }

        [Test]
        public void RegularUserRegistrationTest()
        {
            newUser = UserFactory.GenerateRegularUser();
            RegistrationPage registrationPage = SiteNavigator.NavigateToRegistrationPage(Driver, user);
            HomePage homePage = registrationPage.RegisterUser(newUser);
            Logger.Info("New regular user registered");
            Assert.IsTrue(homePage.OnHeader().AssertMyAppsLinkIsNotPresent());            
        }

        [Test]
        [TestCaseSource(nameof(GetTestUserData))]
        public void MultipleUsersRegistrationTest(string login, string firstname, string lastname, string password, string role)
        {
            newUser = User.GetDefaultUser();
            newUser.Login = login;
            newUser.FirstName = firstname;
            newUser.LastName = lastname;
            newUser.Password   = password;
            newUser.Role = role;
            RegistrationPage registrationPage = SiteNavigator.NavigateToRegistrationPage(Driver, user);
            HomePage homePage = registrationPage.RegisterUser(newUser);
            Logger.Info("New user registered");
            Assert.True(homePage.OnHeader().GetWelcomeText.Contains(newUser.FirstName));
        }

        static System.Collections.Generic.IEnumerable<object[]> GetTestUserData()
        {
            var csvData = File.ReadAllLines(ConfigurationManager.AppSettings["userData"]).Skip(1);

            foreach (var line in csvData) 
            {
                var fields = line.Split(',');
                yield return new object[] { fields[0], fields[1], fields[2], fields[3], fields[4] };
            }
        }
    }
}