using System.Configuration;
using System.Runtime.CompilerServices;
using Selenium.Framework.Pages;

namespace Selenium.Framework
{
    public class SiteNavigator
    {
        public static LoginPage NavigateToLoginPage(Driver driver)
        {
            if (ConfigurationManager.AppSettings["basicAuthentication"] == "true")
            {
                return new LoginPage(driver);
            }
            else
            {
                driver.Navigate().GoToUrl(ConfigurationManager.AppSettings["baseUrl"]);
                return new LoginPage(driver);
            }
        }

        public static RegistrationPage NavigateToRegistrationPage(Driver driver, User user)
        {
            if (ConfigurationManager.AppSettings["basicAuthentication"] == "true")
            {
                driver.Navigate().GoToUrl($"http://{user.Login}:{user.Password}@{ConfigurationManager.AppSettings["basicAuthDomain"]}register");
            }
            else
            {
                driver.Navigate().GoToUrl($"{ConfigurationManager.AppSettings["baseUrl"]}register");
            }
            
            return new RegistrationPage(driver);
        }

        public static NewApplicationPage NavigateToNewApplicationPage(Driver driver)
        {
            if (ConfigurationManager.AppSettings["basicAuthentication"] == "true")
            {
                driver.Navigate().GoToUrl($"{ConfigurationManager.AppSettings["basicAuthBaseUrl"]}new");
            }
            else
            {
                driver.Navigate().GoToUrl($"{ConfigurationManager.AppSettings["baseUrl"]}new");
            }
            return new NewApplicationPage(driver);
        }

        public static HomePage NavigateToHomeUsingBasicAuthentication(Driver driver, User user)
        {
            driver.Navigate().GoToUrl($"http://{user.Login}:{user.Password}@{ConfigurationManager.AppSettings["basicAuthDomain"]}");
            return new HomePage(driver);
        }

        public void RefreshPage(Driver driver)
        {
            driver.Navigate().Refresh();
        }

    }
}