using NUnit.Framework;
using OpenQA.Selenium;

namespace Selenium.Framework.Pages
{
    public class MyApplicationsPage : BasePage
    {
        public MyApplicationsPage(Driver driver) : base(driver)
        {
        }
        public Element newApplicationButton => Driver.FindElement(By.LinkText("Click to add new application"));
        public Element ApplicatiNumberOfDownloads => Driver.FindElement(By.XPath("//*[contains(text(), '# of downloads:')]"));

        public void AssertCanOpenMyApplicationPage()
        {
            Assert.True(newApplicationButton.Displayed);
        }

        public bool ApplicationWasDeleted(string title)
        {
            SiteNavigator navigator = new SiteNavigator();
            navigator.RefreshPage(Driver);
            bool applicationWasDeleted = false;
            try
            {
                Element element = Driver.FindElement(By.XPath($"//*[contains(text(), '{title}')]"));
            }
            catch (WebDriverTimeoutException)
            {
                applicationWasDeleted = true;
            }
            if (applicationWasDeleted == true)
            {
                Logger.Info($"Application {title} was deleted");
            }
            else
            {
                Logger.Info($"Application {title} is still present");
            }
            return applicationWasDeleted;
        }

        public ApplicationPage OpenApplicationByTitle(string title)
        {
            Element ApplicationLocator = Driver.FindElement(By.XPath($"//*[contains(text(), '{title}')]/following-sibling::a"));
            ApplicationLocator.Click();
            return new ApplicationPage(Driver);
        }

        public ApplicationPage OpenApplicationByDescription(string description)
        {
            Element ApplicationLocator = Driver.FindElement(By.XPath($"//*[contains(text(), '{description}')]/following-sibling::a"));
            ApplicationLocator.Click();
            return new ApplicationPage(Driver);
        }
    }
}