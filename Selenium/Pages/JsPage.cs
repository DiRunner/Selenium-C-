using OpenQA.Selenium;

namespace Selenium.Framework.Pages
{
    public class JsPage : BasePage
    {
        public JsPage(Driver driver) : base(driver)
        {
        }

        public Element Topbox => Driver.FindElement(By.Id("top"));
        public Element Leftbox => Driver.FindElement(By.Id("left"));
        public Element Processbutton => Driver.FindElement(By.Id("process"));
        public Element JumpingElement => Driver.FindElement(By.ClassName("flash"));


        public void MakeCoordenatesVerification(string x, string y)
        {
            Topbox.TypeText(x);
            Leftbox.TypeText(y);
            Processbutton.Click();
            Driver.WaitForAjax();
        }        
    }
}