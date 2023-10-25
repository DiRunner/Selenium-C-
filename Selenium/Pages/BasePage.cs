using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace Selenium.Framework.Pages
{
    public class BasePage
    {
        public Driver Driver;

        protected ILog Logger;

        public BasePage(Driver driver)
        {
            this.Driver = driver;
            Logger = LogManager.GetLogger(GetType());
        }
                
        public Element FlashMessage => Driver.FindElement(By.CssSelector(".flash"));

        public string GetFlashMessage() => FlashMessage.Text;

        public Header OnHeader()
        {
            return new Header(Driver);
        }

        public void AcceptPopUpWindow()
        {
            IAlert alert = Driver.SwitchTo().Alert();
            alert.Accept();
        }

        public void AssertAlertMessage(string message)
        {
            IAlert alert = Driver.SwitchTo().Alert();
            string alertMessage = alert.Text;
            Assert.AreEqual(alertMessage, message);
        }
        
        public static void SelectFromDropdown(List<Element> dropdown, string text)
        {
            foreach (Element option in dropdown)
            {
                if (option.Text.Equals(text))
                    option.Click();
            }
        }
        public bool AssertElementIsNotPresent(By locator)
        {
            SiteNavigator navigator = new SiteNavigator();
            navigator.RefreshPage(Driver);
            bool elementIsNotPresent = false;
            try
            {
                Element element = Driver.FindElement(locator);
            }
            catch (WebDriverTimeoutException)
            {
                elementIsNotPresent = true;
            }
            if (elementIsNotPresent == true)
            {
                Logger.Info($"Element with locator {locator} is not present");
            }
            else
            {
                Logger.Info($"Element with locator {locator} is present");
            }
            return elementIsNotPresent;
        }
    }
}