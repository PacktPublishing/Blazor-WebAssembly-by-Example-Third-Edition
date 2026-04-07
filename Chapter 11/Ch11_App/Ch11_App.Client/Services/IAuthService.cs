using Ch11_App.Shared;

namespace Ch11_App.Client.Services
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(LoginRequest loginRequest);
        Task LogoutAsync();
    }
}