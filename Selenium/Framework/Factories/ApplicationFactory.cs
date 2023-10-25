using Selenium.Framework.Utilities;

namespace Selenium.Framework.Factories
{
    public class ApplicationFactory
    {
        public static Application GenerateUniqueApplication(string Category)
        {
            var application = new Application();
            application.Title = UniqueApplicationInfoGenerator.GenerateUniqueTitleTimestamp();
            application.Description = UniqueApplicationInfoGenerator.GenerateUniqueDescriptionNameTimestamp();
            application.Category = Category;
            return application;
        }
    }
}