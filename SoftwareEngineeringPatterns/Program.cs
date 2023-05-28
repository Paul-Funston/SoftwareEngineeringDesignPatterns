using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

UserFactory newFactory = new UserFactory();
Object testAdmin = new { TwoFactorAuthentication = true, IsAdmin = true };
Object testUser= new { TwoFactorAuthentication = true };
Object testUserTwo = new { TwoFactorNotRequired = true };
Object testAdminTwo = new { TwoFactorNotRequired = true, IsAdmin = true };

User newAdmin = newFactory.CreateUser(JsonSerializer.Serialize(testAdmin));
User newUser = newFactory.CreateUser(JsonSerializer.Serialize(testUser));
User newUserTwo = newFactory.CreateUser(JsonSerializer.Serialize(testUserTwo));
User newAdminTwo = newFactory.CreateUser(JsonSerializer.Serialize(testAdminTwo));


Console.WriteLine(newAdmin);
Console.WriteLine(newUser);
Console.WriteLine(newUserTwo);
Console.WriteLine(newAdminTwo);

try
{
    Object failUser = new { };
    User newFailUser = newFactory.CreateUser(JsonSerializer.Serialize(failUser));
    Console.WriteLine(newFailUser);
} catch (Exception ex) { Console.WriteLine(ex.Message); }


public class User
{
    protected string _password;

    protected IAuthentication _authentication { get; set; }
    protected void PasswordHash()
    {

    }

    public User() { }
    public User (bool HasTwoFactorAuthentication)
    {
        if (HasTwoFactorAuthentication)
        {
            _authentication = new TwoFactorAuthentication();
        } else
        {
            _authentication = new BasicAuthentication();
        }
    }


}

public class Admin : User
{
    public Admin() { }
    public Admin(bool HasTwoFactorAuthentication) :base(HasTwoFactorAuthentication) { }
}


public class UserFactory
{

    public virtual User CreateUser(string obj)
    {

        bool hasTwoFactorNotRequired = false;
        bool hasTwoFactorAuthentication = false;
        bool IsAdmin = false;
        
        
        try
        {
            // Extract Options from Json
            JsonDocument options = JsonDocument.Parse(obj);
            hasTwoFactorNotRequired = _parseJsonProperty(options, "TwoFactorNotRequired");
            hasTwoFactorAuthentication = _parseJsonProperty(options, "TwoFactorAuthentication");
            IsAdmin = _parseJsonProperty(options, "IsAdmin");

        } catch { }

        if (!hasTwoFactorNotRequired && !hasTwoFactorAuthentication)
        {
            throw new Exception("Authentication type is required");
        }

        if (IsAdmin)
        {
            return new Admin(hasTwoFactorAuthentication);
        } else
        {
            return new User(hasTwoFactorAuthentication);
        }
    }

    private bool _parseJsonProperty(JsonDocument obj, string property)
    {
        try
        {
            return obj.RootElement.GetProperty(property).GetBoolean();
        } catch
        {
            return false;
        }
    }
}


public interface IAuthentication
{

}

public class TwoFactorAuthentication : IAuthentication { }
public class BasicAuthentication : IAuthentication { }