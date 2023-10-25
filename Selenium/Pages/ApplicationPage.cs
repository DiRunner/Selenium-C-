using NUnit.Framework;
using OpenQA.Selenium;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace Selenium.Framework.Pages
{
    public class ApplicationPage : BasePage
    {
        public ApplicationPage(Driver driver) : base(driver)
        {
        }

        public Element ApplicationTitle => Driver.FindElement(By.ClassName("name"));

        public Element ApplicationText(string text) => Driver.FindElement(By.XPath($"//*[contains(text(), '{text}')]"));

        public Element DownloadButton => Driver.FindElement(By.CssSelector(".download-button a"));

        public Element EditApplicationButton => Driver.FindElement(By.XPath("//div[contains(@class, 'edit-app-button')]//a[contains(text(), 'Edit')]"));

        public Element DeleteApplicationButton => Driver.FindElement(By.XPath("//div[contains(@class, 'edit-app-button')]//a[contains(text(), 'Delete')]"));

        public ApplicationJsonPage ClickOnDownloadApplication()
        {
            DownloadButton.Click();
            return new ApplicationJsonPage(Driver);
        }

        public ApplicationPage DeleteApplication()
        {
            DeleteApplicationButton.Click();
            this.AcceptPopUpWindow();
            return this;
        }

        public void DownloadApplication()
        {
            DownloadButton.Click();
            Driver.Navigate().Back();            
        }

        public EditApplicationPage ClickOnEditApplication()
        {
            EditApplicationButton.Click();
            return new EditApplicationPage(Driver);
        }
        
        public Application GetCurrentApplicationInformation()
        {
            var application = new Application();
            application.Title = ApplicationTitle.Text;
            application.Description = ExtractContentFromApllicationInfoElement(ApplicationText("Description:").Text);
            application.Category = ExtractContentFromApllicationInfoElement(ApplicationText("Category:").Text);
            application.Author = ExtractContentFromApllicationInfoElement(ApplicationText("Author:").Text);
            application.NumberOfDownloads = ExtractContentFromApllicationInfoElement(ApplicationText("# of downloads:").Text);
            return application;
        }

        public ApplicationPage AsserApplicationInformation(Application expectedApplication, User user)
        {
            Application currentApplication = GetCurrentApplicationInformation();
            Assert.Multiple(() =>
            {
                Assert.AreEqual(currentApplication.Title, expectedApplication.Title);
                Assert.AreEqual(currentApplication.Author, user.Login);
                Assert.AreEqual(currentApplication.Description, expectedApplication.Description);
                Assert.AreEqual(currentApplication.Category, expectedApplication.Category);
            });            
            return this;
        }

        public ApplicationPage AsserApplicationInformation(Application expectedApplication, Application currentApplication,  User user)
        {
            Assert.Multiple(() =>
            {
                Assert.AreEqual(currentApplication.Title, expectedApplication.Title);
                Assert.AreEqual(currentApplication.Author, user.Login);
                Assert.AreEqual(currentApplication.Description, expectedApplication.Description);
                Assert.AreEqual(currentApplication.Category, expectedApplication.Category);
            });
            return this;
        }

        public ApplicationPage AsserApplicationInformationWithouTitle(Application expectedApplication, User user)
        {
            Application currentApplication = GetCurrentApplicationInformation();
            Assert.Multiple(() =>
            {
                Assert.AreEqual(currentApplication.Author, user.Login);
                Assert.AreEqual(currentApplication.Description, expectedApplication.Description);
                Assert.AreEqual(currentApplication.Category, expectedApplication.Category);
            });            
            return this;
        }        

        public String ExtractContentFromApllicationInfoElement(string text)
        {
            int postion = text.IndexOf(':');
            string textContent = text.Substring(postion + 1);
            if (textContent.StartsWith(" "))
            {
                return textContent.Substring(1);
            }
            else
            {
                return textContent;
            }
        }

    }
}
