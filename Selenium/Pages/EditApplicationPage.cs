using OpenQA.Selenium;
using System.Collections.Generic;

namespace Selenium.Framework.Pages
{
    public class EditApplicationPage : BasePage
    {
        public EditApplicationPage(Driver driver) : base(driver)
        {
        }

        public Element ApplicationDescriptionBox => Driver.FindElement(By.Name("description"));

        public List<Element> ApplicationCategoryDropdown => Driver.FindElements(By.XPath("//select[contains(@name, 'category')]//option"));

        public Element ApplicationImageButton => Driver.FindElement(By.Name("Image')]"));

        public Element ApplicatiIconButton => Driver.FindElement(By.Name("icon"));

        public Element UpdateApplicationButton => Driver.FindElement(By.CssSelector("[value='Update']"));

        public void UpdateApplicationWithUniqueInformation(Application application)
        {
            ApplicationDescriptionBox.TypeText(application.Description);
            SelectFromDropdown(ApplicationCategoryDropdown, application.Category);
            UpdateApplicationButton.Click();
        }
    }
}
