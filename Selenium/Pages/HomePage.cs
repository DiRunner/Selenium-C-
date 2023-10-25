using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Selenium.Framework.Pages
{
    public class HomePage : BasePage
    {
        public HomePage(Driver driver) : base(driver)
        {
        }

        public List<Element> PopularApplications => Driver.FindElements(By.ClassName("popular-app"));
        public List<Element> Applications => Driver.FindElements(By.LinkText("Details"));

        public ApplicationPage OpenPopularApplicationByIndex(int index)
        {
            Applications[index].Click();
            return  new ApplicationPage(Driver);
        }

        public ApplicationPage OpenApplicationByIndex(int index)
        {
            Applications[index].Click();
            return new ApplicationPage(Driver);
        }

        public ApplicationPage OpenApplicationIsInPopularApps(Application application)
        {
            IEnumerable<Element> filteredElements = PopularApplications.Where(e => e.Text.Contains(application.Title));

            ReadOnlyCollection<Element> readonlyFilteredElements = new ReadOnlyCollection<Element>(filteredElements.ToList());
            readonlyFilteredElements[0].Click();
            return new ApplicationPage(Driver);
        }

        public bool AssertApplicationIsInPopularApps(Application application)
        {
            IEnumerable<Element> filteredElements = PopularApplications.Where(e => e.Text.Contains(application.Title));

            ReadOnlyCollection<Element> readonlyFilteredElements = new ReadOnlyCollection<Element>(filteredElements.ToList());
            return readonlyFilteredElements.Count == 1;
        }
    }
}