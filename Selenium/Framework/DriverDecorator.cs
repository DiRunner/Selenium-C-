using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenQA.Selenium;

namespace Selenium.Framework
{
    public abstract class DriverDecorator : Driver
    {
        protected readonly Driver Driver;

        protected DriverDecorator(Driver driver)
        {
            Driver = driver;
        }

        public override void Start(Browser browser)
        {
            Driver?.Start(browser);
        }

        public override void Quit()
        {
            Driver?.Quit();
        }

        public override void GoToUrl(string url)
        {
            Driver?.GoToUrl(url);
        }

        public override Element FindElement(By locator)
        {
            return Driver?.FindElement(locator);
        }

        public override List<Element> FindElements(By locator)
        {
            return Driver?.FindElements(locator);
        }

        public override ReadOnlyCollection<string> WindowHandles
        {get 
            {
                return Driver?.WindowHandles;
            }
        }

        public override void WaitForAjax()
        {
            Driver?.WaitForAjax();
        }

        public override void WaitUntilPageLoadsCompletely()
        {
            Driver?.WaitUntilPageLoadsCompletely();
        }

        public override void OpenNewTabWithUrl(string url)
        {
            Driver?.OpenNewTabWithUrl(url);
        }

        public override string GetElementAttributeUsingJavaScript(string sentence, By locator)
        {
            return Driver?.GetElementAttributeUsingJavaScript(sentence, locator);
        }

        public override void CaptureScreenshot(string fileName)
        {
            Driver?.CaptureScreenshot(fileName);
        }

        public override IOptions Manage()
        {
            return Driver?.Manage();
        }

        public override INavigation Navigate()
        {
            return Driver?.Navigate();
        }

        public override ITargetLocator SwitchTo()
        {
            return Driver?.SwitchTo();
        }
    }
}
