using OpenQA.Selenium;
using System.Collections.Generic;
using System.Configuration;

namespace Selenium.Framework.Pages
{
    public class RegistrationPage : BasePage
    {
        public RegistrationPage(Driver driver) : base(driver)
        {
        }

        public Element UsernameBox => Driver.FindElement(By.Name("name"));
        
        public Element FirstNameBox => Driver.FindElement(By.Name("fname"));

        public Element LastNameBox => Driver.FindElement(By.Name("lname"));

        public Element PasswordBox => Driver.FindElement(By.Name("password"));

        public Element PasswordConfirmBox => Driver.FindElement(By.Name("passwordConfirm"));

        public List<Element> RoleDropdown => Driver.FindElements(By.XPath("//select[contains(@name, 'role')]//option"));

        public Element RegisterBtn => Driver.FindElement(By.CssSelector("[value='Register']"));

        #region Methods

        public HomePage RegisterUser(User user)
        {
            UsernameBox.TypeText(user.Login);
            FirstNameBox.TypeText(user.FirstName);
            LastNameBox.TypeText(user.LastName);
            PasswordBox.TypeText(user.Password);
            PasswordConfirmBox.TypeText(user.Password);
            SelectFromDropdown(RoleDropdown, user.Role);
            RegisterBtn.Click();
            if (ConfigurationManager.AppSettings["basicAuthentication"] == "true")
            {
                HomePage homePage = new HomePage(Driver);
                LoginPage loginPage = new LoginPage(Driver);    
                homePage.OnHeader().LogOutLink.Click();
                loginPage.RegularLogin(user);
            }
            return new HomePage(Driver);
        }
        #endregion        
    }
}