using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace Selenium.Framework.Pages
{
    public class NewApplicationPage : BasePage
    {
        public NewApplicationPage(Driver driver) : base(driver)
        {
        }

        public Element ApplicationTitleBox => Driver.FindElement(By.Name("title"));

        public Element ApplicationDescriptionBox => Driver.FindElement(By.Name("description"));

        public List<Element> ApplicationCategoryDropdown => Driver.FindElements(By.XPath("//select[contains(@name, 'category')]//option"));

        public Element ApplicationImageButton => Driver.FindElement(By.Name("image"));

        public Element ApplicatiIconButton => Driver.FindElement(By.Name("icon"));

        public Element CreateApplicationButton => Driver.FindElement(By.CssSelector("[value='Create']"));

        public MyApplicationsPage CreateNewUniqueApplication(Application application)
        {
            ApplicationTitleBox.TypeText(application.Title);
            ApplicationDescriptionBox.TypeText(application.Description);
            SelectFromDropdown(ApplicationCategoryDropdown, application.Category);
            CreateApplicationButton.Click();
            return new MyApplicationsPage(Driver);
        }

        public MyApplicationsPage CreateNewUniqueApplicationWithImageAndIcon(Application application)
        {
            ApplicationTitleBox.TypeText(application.Title);
            ApplicationDescriptionBox.TypeText(application.Description);
            SelectFromDropdown(ApplicationCategoryDropdown, application.Category);
            String fileFullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["image"]);
            ApplicationImageButton.TypeText(fileFullPath);
            ApplicatiIconButton.TypeText(fileFullPath);
            CreateApplicationButton.Click();
            return new MyApplicationsPage(Driver);
        }
    }
}
