using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V109.DOM;
using System.Collections.Generic;

namespace Selenium.Framework.Pages
{
    public class ApplicationJsonPage : BasePage
    {
        public ApplicationJsonPage(Driver driver) : base(driver)
        {
        }
        private Element RawDataButton => Driver.FindElement(By.Id("rawdata-tab"));
        
        private Element JsonElementChrome => Driver.FindElement(By.XPath("//*[contains(text(), 'imageData')]"));

        private Element JsonElementsFirefox => Driver.FindElement(By.ClassName("data"));


        public Element GetJsonElement ()
        {
            if (Settings.GetDriver() == Browser.Chrome)
            {
                return JsonElementChrome;
            }
            else
            {
                return JsonElementsFirefox;
            }
        }
        public void ClickRawDataButton()
        {
            if (Settings.GetDriver() == Browser.Firefox)
            {
                RawDataButton.Click();
            }
        }
    }
}
