using System;
using System.Collections.Generic;
using log4net;
using OpenQA.Selenium;

namespace Selenium.Framework
{
    public class LoggingDriver : DriverDecorator
    {
        protected ILog Logger;
        public LoggingDriver(Driver driver)
        : base(driver)
        {
            Logger = LogManager.GetLogger(GetType());
        }

        public override void Start(Browser browser)
        {
            Logger.Info($"Start browser = {Enum.GetName(typeof(Browser), browser)}");
            Driver?.Start(browser);
        }

        public override void Quit()
        {
            Logger.Info("browser Quit");
            Driver?.Quit();
        }

        public override void Close()
        {
            Logger.Info("Close browser");
            Driver?.Quit();
        }

        public override void GoToUrl(string url)
        {
            Logger.Info($"Go to URL = {url}");
            Driver?.GoToUrl(url);
        }

        public override void OpenNewTabWithUrl(string url)
        {
            Logger.Info($"Open a new tab with the URL = {url}");
            Driver?.OpenNewTabWithUrl(url);
        }

        public override void CaptureScreenshot(string fileName)
        {
            Logger.Info($"Screenshot saved to: {fileName}");
            Driver?.CaptureScreenshot(fileName);
        }

        public override Element FindElement(By locator)
        {
            Logger.Info("Find Element");
            return Driver?.FindElement(locator);
        }

        public override List<Element> FindElements(By locator)
        {
            Logger.Info("Find elements");
            return Driver?.FindElements(locator);
        }
    }
}
