using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenQA.Selenium;

namespace Selenium.Framework
{
    public abstract class Driver
    {
        public abstract void Start(Browser browser);
        public abstract void Quit();
        public abstract void Close();
        public abstract void GoToUrl(string url);
        public abstract Element FindElement(By locator);
        public abstract List<Element> FindElements(By locator);
        public abstract ReadOnlyCollection<string> WindowHandles { get; }
        public abstract void WaitForAjax();
        public abstract void WaitUntilPageLoadsCompletely();
        public abstract void OpenNewTabWithUrl(string url);
        public abstract string GetElementAttributeUsingJavaScript(string sentence, By locator);

        public abstract void CaptureScreenshot(string fileName); 
        public abstract IOptions Manage();
        public abstract INavigation Navigate();
        public abstract ITargetLocator SwitchTo();
    }
}
