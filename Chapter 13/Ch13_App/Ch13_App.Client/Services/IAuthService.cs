using Ch13_App.Shared;

namespace Ch13_App.Client.Services
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(LoginRequest loginRequest);
        Task LogoutAsync();
    }
}