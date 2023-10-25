using OpenQA.Selenium;

namespace Selenium.Framework.Pages
{
    public class AjaxPage : BasePage
    {
        public AjaxPage(Driver driver) : base(driver)
        {
        }
        public Element Xbox => Driver.FindElement(By.Id("x"));
        public Element Ybox => Driver.FindElement(By.Id("y"));
        public Element Sumbutton => Driver.FindElement(By.Id("calc"));
        public Element ResultText => Driver.FindElement(By.Id("result"));


        public string MakeSumeWithSimpleCalculator(string x, string y)
        {
            Xbox.TypeText(x);
            Ybox.TypeText(y);
            Sumbutton.Click();
            Driver.WaitForAjax();
            return ResultText.Text;
        }        
    }
}