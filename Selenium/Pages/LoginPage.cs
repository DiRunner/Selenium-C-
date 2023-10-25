using OpenQA.Selenium;
using System.Configuration;

namespace Selenium.Framework.Pages
{
    public class LoginPage : BasePage
    {
        public LoginPage(Driver driver) : base(driver)
        {
        }

        public Element UsernameBox => Driver.FindElement(By.Id("j_username"));
        
        public Element PasswordBox => Driver.FindElement(By.Id("j_password"));
        
        public Element LoginButton => Driver.FindElement(By.XPath("//input[@value='Login']"));
        
        public Element RegisterLink => Driver.FindElement(By.PartialLinkText("Register"));

        #region Methods

        public HomePage Login(User user)
        {
            if(ConfigurationManager.AppSettings["basicAuthentication"] == "true")
            {
                BasicAuthenticationLogin(user);
            }
            else
            {
                RegularLogin(user);
            }
            return new HomePage(Driver);
        }

        public void BasicAuthenticationLogin(User user)
        {
            SiteNavigator.NavigateToHomeUsingBasicAuthentication(Driver, user);
        }

        public void RegularLogin(User user)
        {
            UsernameBox.TypeText(user.Login);
            PasswordBox.TypeText(user.Password);
            LoginButton.Click();
        }
        #endregion
    }
}