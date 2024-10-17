
namespace OoberEats.Application.Services;

public class AuthService : IAuthService
{
    public async Task<AuthenticationResult> LoginAsync(string Email, string Password)
    {
        return await Task.FromResult(new AuthenticationResult(Guid.NewGuid(), string.Empty, string.Empty, Email, Password, "token"));
    }

    public async Task<AuthenticationResult> RegisterAsync(string FirstName, string LastName, string Email, string Password)
    {
        return await Task.FromResult(new AuthenticationResult(Guid.NewGuid(), string.Empty, string.Empty, Email, Password, "token"));
    }
}

public interface IAuthService
{
    Task<AuthenticationResult> LoginAsync(string Email, string Password);
    Task<AuthenticationResult> RegisterAsync(string FirstName, string LastName, String Email, String Password);
}

