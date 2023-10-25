public class User
{  
    public string Login { get; set; }

    public string Password { get; set; }
        
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Role { get; set; }

    public static User GetDefaultUser()
    {
        return new User { Login = "admin", Password = "admin", FirstName = "Ivan", LastName = "Petrov", Role = "User" };
    }

}