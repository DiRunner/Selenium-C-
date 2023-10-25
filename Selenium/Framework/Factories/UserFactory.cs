using Selenium.Framework.Utilities;

namespace Selenium.Framework.Factories
{
    public class UserFactory
    {
        private static string DEFAULT_PASSWORD = "DefaultPass";
        
        private static User GenerateUser(string role)
        {
            var user = new User();
            user.Login = UniqueUserNameGenerator.GenerateUniqueNameTimestamp();
            user.FirstName = UniqueUserNameGenerator.GenerateUniqueNameTimestamp();
            user.LastName = UniqueUserNameGenerator.GenerateUniqueLastNameTimestamp();
            user.Password = DEFAULT_PASSWORD;
            user.Role = role;
            return user;
        }

        public static User GenerateRegularUser()
        {
            return GenerateUser("USER");
        }

        public static User GenerateDeveloperUser()
        {
            return GenerateUser("DEVELOPER");
        }




    }
}