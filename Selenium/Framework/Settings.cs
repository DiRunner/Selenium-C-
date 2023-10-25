using System;
using System.Configuration;

namespace Selenium.Framework
{
    public class Settings
    {
        public static string GetBaseUrl()
        {
            return ConfigurationManager.AppSettings["baseUrl"];
        }

        public static Browser GetDriver()
        {
            switch (GetBrowserType())
            {
                case "firefox":
                    return Browser.Firefox;

                case "chrome":
                    return Browser.Chrome;                

                default:
                    throw new Exception("Unknown browser type!");
            }
            
        }

        public static string GetBrowserType()
        {
            return ConfigurationManager.AppSettings["browserType"];
        }
    }
}