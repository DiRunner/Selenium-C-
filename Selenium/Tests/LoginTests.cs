using NUnit.Framework;
using System.Configuration;
using System.Linq;
using Selenium.Framework.Pages;

namespace Selenium.Framework.Tests
{
    public class LoginTests : BaseTest
    {
        private User user;
        private Header header;
        private LoginPage loginPage;

        [SetUp]
        protected void Initialize()
        {
            user = User.GetDefaultUser();
            header = new Header(Driver);
            loginPage = new LoginPage(Driver);
        }

        [Test]
        [Category("Regression")]
        public void ValidLoginTest()
        {
            HomePage homePage = SiteNavigator.NavigateToLoginPage(Driver).Login(user);
            Logger.Info("Assert user login");
            Assert.True(homePage.OnHeader().GetWelcomeText.Contains(user.FirstName));
        }

        [Test]
        [Category("Smoke")]
        public void InvalidLoginTest()
        {
            user.Password = "invalid";
            LoginPage loginPage = SiteNavigator.NavigateToLoginPage(Driver);
            HomePage homePage = loginPage.Login(user);
            if (ConfigurationManager.AppSettings["basicAuthentication"] == "true")
            {
                loginPage.AssertElementIsNotPresent(homePage.OnHeader().WelcomeLabelLocator);
            }
            else
            {
                Assert.True(loginPage.GetFlashMessage().Contains("invalid username or password"));
            }            
        }

        [Test]
        public void Logout()
        {
            HomePage homePage = SiteNavigator.NavigateToLoginPage(Driver).Login(user);
            Logger.Info("user logged");
            
            if (ConfigurationManager.AppSettings["basicAuthentication"] == "true")
            {
                Driver.OpenNewTabWithUrl(ConfigurationManager.AppSettings["basicAuthBaseUrl"]);
            }
            else
            {
                Driver.OpenNewTabWithUrl(ConfigurationManager.AppSettings["baseUrl"]);
            }
            homePage.OnHeader().LogOutLink.Click();
            Driver.SwitchTo().Window(Driver.WindowHandles.First());
            homePage.OpenApplicationByIndex(0);
            if (ConfigurationManager.AppSettings["basicAuthentication"] == "true")
            {
                loginPage.AssertElementIsNotPresent(homePage.OnHeader().WelcomeLabelLocator);
            }
            else
            {
                Assert.True(loginPage.UsernameBox.Displayed);
                Assert.True(loginPage.PasswordBox.Displayed);
            }            
        }
    }
}