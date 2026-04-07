using ClaimsDashboard.Shared;

namespace ClaimsDashboard.Client.Interfaces
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(LoginRequest loginRequest);
        Task LogoutAsync();
    }
}
