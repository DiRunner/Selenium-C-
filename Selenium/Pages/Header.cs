using OpenQA.Selenium;

namespace Selenium.Framework.Pages
{
    public class Header : BasePage
    {
        public Header(Driver driver) : base(driver)
        {
        }

        public Element WelcomeLabel => Driver.FindElement(WelcomeLabelLocator);

        public By WelcomeLabelLocator => By.CssSelector(".welcome");

        public Element HomeLink => Driver.FindElement(By.LinkText("Home"));

        public Element LogOutLink => Driver.FindElement(By.LinkText("Logout"));

        public Element MyApplicationsLink => Driver.FindElement(By.LinkText("My applications"));

        private By MyApplicationsLinkLocator => By.LinkText("My applications");

        public Element AjaxTestLink => Driver.FindElement(By.LinkText("Ajax test page"));

        public Element JsTestLink => Driver.FindElement(By.LinkText("JS test page"));

        #region Methods

        public string GetWelcomeText => WelcomeLabel.Text;

        public LoginPage Logout()
        {
            LogOutLink.Click();
            return new LoginPage(Driver);
        }
        #endregion
        public bool AssertMyAppsLinkIsNotPresent()
        {
            SiteNavigator navigator = new SiteNavigator();
            navigator.RefreshPage(Driver);
            bool applicationWasDeleted = false;
            try
            {
                Element element = Driver.FindElement(MyApplicationsLinkLocator);
            }
            catch (WebDriverTimeoutException)
            {
                applicationWasDeleted = true;
            }
            if (applicationWasDeleted == true)
            {
                Logger.Info($"Element with locator {MyApplicationsLinkLocator} is not present");
            }
            else
            {
                Logger.Info($"Element with locator {MyApplicationsLinkLocator} is present");
            }
            return applicationWasDeleted;
        }
        public MyApplicationsPage ClickOnMyApplicationsLink()
        {
            MyApplicationsLink.Click();
            return new MyApplicationsPage(Driver);
        }

        public AjaxPage ClickOnAjaxTestLink()
        {
            AjaxTestLink.Click();
            return new AjaxPage(Driver);
        }

        public JsPage ClickOnJsTestLink()
        {
            JsTestLink.Click();
            return new JsPage(Driver);
        }
    }
}