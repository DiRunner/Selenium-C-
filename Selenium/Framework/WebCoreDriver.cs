using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace Selenium.Framework
{
    public class WebCoreDriver : Driver
    {
        private IWebDriver _webDriver;
        private WebDriverWait _webDriverWait;

        public override void Start(Browser browser)
        {
            switch (browser)
            {
                case Browser.Chrome:
                    _webDriver = new ChromeDriver(Environment.CurrentDirectory);
                    break;
                case Browser.Firefox:
                    _webDriver = new FirefoxDriver(Environment.CurrentDirectory);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(browser), browser, null);
            }

            _webDriverWait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(30));
        }

        public override void Quit()
        {
            _webDriver?.Quit();
        }

        public override void Close()
        {
            _webDriver?.Close();
        }

        public override void GoToUrl(string url)
        {
            _webDriver.Navigate().GoToUrl(url);
        }

        public override Element FindElement(By locator)
        {
            IWebElement nativeWebElement = 
                _webDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator));
            Element element = new WebElement(_webDriver, nativeWebElement, locator);

            Element logElement = new LogElement(element);

            return logElement;
        }

        public override List<Element> FindElements(By locator)
        {
            ReadOnlyCollection<IWebElement> nativeWebElements = 
                _webDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.PresenceOfAllElementsLocatedBy(locator));
            var elements = new List<Element>();
            foreach (var nativeWebElement in nativeWebElements)
            {
                Element element = new WebElement(_webDriver, nativeWebElement, locator);
                elements.Add(element);
            }

            return elements;
        }

        public override ReadOnlyCollection<string> WindowHandles 
        { get
            {
                return _webDriver?.WindowHandles;
            }
        }
        public override void WaitForAjax()
        {
            var js = (IJavaScriptExecutor)_webDriver;
            _webDriverWait.Until(wd => js.ExecuteScript("return jQuery.active").ToString() == "0");
        }        

        public override void WaitUntilPageLoadsCompletely()
        {
            var js = (IJavaScriptExecutor)_webDriver;
            _webDriverWait.Until(wd => js.ExecuteScript("return document.readyState").ToString() == "complete");
        }

        public override void OpenNewTabWithUrl(string url)
        {
            ((IJavaScriptExecutor)_webDriver).ExecuteScript("window.open();");
            _webDriver.SwitchTo().Window(_webDriver.WindowHandles.Last());
            _webDriver.Navigate().GoToUrl(url);
        }

        public override string GetElementAttributeUsingJavaScript(string sentence, By locator )
        {
            IWebElement element = _webDriver.FindElement(locator);
            var js = (IJavaScriptExecutor)_webDriver;
            return js.ExecuteScript(sentence, element).ToString();
        }

        public override void CaptureScreenshot(string fileName)
        {
            var screenshot = ((ITakesScreenshot)_webDriver).GetScreenshot();
            screenshot.SaveAsFile(fileName, ScreenshotImageFormat.Png);           
        }

        public override IOptions Manage()
        {
           return _webDriver?.Manage();
        }

        public override INavigation Navigate()
        {
            return _webDriver?.Navigate();
        }

        public override ITargetLocator SwitchTo()
        {
            return _webDriver?.SwitchTo();
        }
    }
}
