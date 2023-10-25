using NUnit.Framework;
using Newtonsoft.Json.Linq;
using Selenium.Framework.Factories;
using Selenium.Framework.Pages;

namespace Selenium.Framework.Tests
{
    public class ApplicationTests : BaseTest
    {
        private User user;
        private Application application;

        [SetUp]
        protected void Initialize()
        {
            user = User.GetDefaultUser();
            application = new Application();            
        }

        [Test]
        public void VerifyApplicationInformationTest()
        {
            HomePage homePage = SiteNavigator.NavigateToLoginPage(Driver).Login(user);
            ApplicationPage applicationPage = homePage.OpenApplicationByIndex(1);
            application = applicationPage.GetCurrentApplicationInformation();
            ApplicationJsonPage applicationJsonPage = applicationPage.ClickOnDownloadApplication();
            applicationJsonPage.ClickRawDataButton();            
            JObject json = JObject.Parse(applicationJsonPage.GetJsonElement().Text);
            Assert.Multiple(() =>
            {
                Assert.AreEqual(application.Title, (string)json["title"]);
                Assert.AreEqual(application.Author, (string)json["author"]["name"]);
                Assert.AreEqual(application.Category, (string)json["category"]["title"]);
                Assert.AreEqual(application.Description, (string)json["description"]);
                Assert.AreEqual(application.NumberOfDownloads, ((int)json["numberOfDownloads"] - 1).ToString());
            });            
        }

        [Test]
        public void CreateApplicationWithoutImagesTest()
        {
            Application newApplication = ApplicationFactory.GenerateUniqueApplication("Information");
            SiteNavigator.NavigateToLoginPage(Driver).Login(user);
            MyApplicationsPage MyApplicationsPage = SiteNavigator.NavigateToNewApplicationPage(Driver).CreateNewUniqueApplication(newApplication);
            ApplicationPage applicationPage = MyApplicationsPage.OpenApplicationByTitle(newApplication.Title);
            Application currentApplication = applicationPage.GetCurrentApplicationInformation();            
            ApplicationJsonPage applicationJsonPage = applicationPage.ClickOnDownloadApplication();
            applicationJsonPage.ClickRawDataButton();

            Assert.Multiple(() =>
            {
                applicationPage.AsserApplicationInformation(newApplication, currentApplication, user);
                Assert.IsTrue(applicationJsonPage.GetJsonElement().Displayed);
            });            
        }

        [Test]
        public void EditApplicationWithoutImagesTest()
        {
            HomePage homePage = SiteNavigator.NavigateToLoginPage(Driver).Login(user);
            ApplicationPage applicationPage = homePage.OpenApplicationByIndex(1);
            EditApplicationPage editApplicationPage = applicationPage.ClickOnEditApplication();
            application = ApplicationFactory.GenerateUniqueApplication("Games");
            editApplicationPage.UpdateApplicationWithUniqueInformation(application);
            MyApplicationsPage myApplicationsPage= homePage.OnHeader().ClickOnMyApplicationsLink();
            myApplicationsPage.OpenApplicationByDescription(application.Description);
            applicationPage.AsserApplicationInformationWithouTitle(application, user);
        }

        [Test]
        public void CreateApplicationWithImageAndIconTest()
        {
            Application newApplication = ApplicationFactory.GenerateUniqueApplication("Information");
            SiteNavigator.NavigateToLoginPage(Driver).Login(user);
            MyApplicationsPage myApplicationsPage = SiteNavigator.NavigateToNewApplicationPage(Driver).CreateNewUniqueApplicationWithImageAndIcon(newApplication);
            ApplicationPage applicationPage = myApplicationsPage.OpenApplicationByTitle(newApplication.Title);
            Application currentApplication = applicationPage.GetCurrentApplicationInformation();
            ApplicationJsonPage applicationJsonPage = applicationPage.ClickOnDownloadApplication();
            applicationJsonPage.ClickRawDataButton();

            Assert.Multiple(() =>
            {
                applicationPage.AsserApplicationInformation(newApplication, currentApplication, user);
                Assert.IsTrue(applicationJsonPage.GetJsonElement().Displayed);
            });            
        }

        [Test]
        public void DownloadApplicationMutipleTimesTest()
        {
            application = ApplicationFactory.GenerateUniqueApplication("News");
            HomePage homePage = SiteNavigator.NavigateToLoginPage(Driver).Login(user);
            MyApplicationsPage myApplicationsPage = SiteNavigator.NavigateToNewApplicationPage(Driver).CreateNewUniqueApplication(application);
            ApplicationPage applicationPage = new ApplicationPage(Driver);
            do
            {
                myApplicationsPage.OpenApplicationByTitle(application.Title);
                applicationPage.DownloadApplication();
                homePage.OnHeader().HomeLink.Click();
            } while (homePage.AssertApplicationIsInPopularApps(application) == false);
            homePage.OpenApplicationIsInPopularApps(application);
            applicationPage.AsserApplicationInformation(application, user);
        }

        [Test]
        public void DeleteApplicationTest()
        {
            application = ApplicationFactory.GenerateUniqueApplication("News");
            HomePage homePage = SiteNavigator.NavigateToLoginPage(Driver).Login(user);
            MyApplicationsPage myApplicationsPage = SiteNavigator.NavigateToNewApplicationPage(Driver).CreateNewUniqueApplication(application);
            ApplicationPage applicationPage = myApplicationsPage.OpenApplicationByTitle(application.Title);
            applicationPage.DeleteApplication();
            homePage.OnHeader().ClickOnMyApplicationsLink();
            Assert.True(myApplicationsPage.ApplicationWasDeleted(application.Title));
        }
    }
}