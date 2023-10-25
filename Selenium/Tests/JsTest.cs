using NUnit.Framework;
using Selenium.Framework.Pages;
using OpenQA.Selenium;

namespace Selenium.Framework.Tests
{
    public class JsTests : BaseTest
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
        public void coordinatesTest()
        {
            HomePage homePage = SiteNavigator.NavigateToLoginPage(Driver).Login(user);
            JsPage JsPage = homePage.OnHeader().ClickOnJsTestLink();
            string offsetTop = Driver.GetElementAttributeUsingJavaScript("return arguments[0].offsetTop;", By.ClassName("flash"));
            string offsetleft = Driver.GetElementAttributeUsingJavaScript("return arguments[0].offsetLeft;", By.ClassName("flash"));
            JsPage.Topbox.TypeText(offsetTop);
            JsPage.Leftbox.TypeText(offsetleft);
            JsPage.Processbutton.Click();
            JsPage.AssertAlertMessage("Whoo Hoooo! Correct!");
        }       
    }
}