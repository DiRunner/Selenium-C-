using NUnit.Framework;
using Selenium.Framework.Pages;

namespace Selenium.Framework.Tests
{
    public class AjaxTests : BaseTest
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
        public void ValidNumbersTest()
        {
            HomePage homePage = SiteNavigator.NavigateToLoginPage(Driver).Login(user);
            AjaxPage ajaxPage = homePage.OnHeader().ClickOnAjaxTestLink();
            string result = ajaxPage.MakeSumeWithSimpleCalculator("10", "20");
            double expectedResult = 30.0;
            Assert.AreEqual($"Result is: {expectedResult:F1}", result);
        }

        [Test]
        public void InvalidInputTest()
        {
            HomePage homePage = SiteNavigator.NavigateToLoginPage(Driver).Login(user);
            AjaxPage ajaxPage = homePage.OnHeader().ClickOnAjaxTestLink();
            string result = ajaxPage.MakeSumeWithSimpleCalculator("10", "invalid input");
            string expectedResult = "Incorrect data";
            Assert.AreEqual($"Result is: {expectedResult}", result);
        }
    }
}