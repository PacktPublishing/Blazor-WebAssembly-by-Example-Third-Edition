using ClaimsDashboard.Client.Interfaces;
namespace ClaimsDashboard.Client.Services
{
    public class TokenStorageService : ITokenStorageService
    {
        private string? storedToken;

        public Task SetTokenAsync(string token)
        {
            storedToken = token;
            return Task.CompletedTask;
        }

        public Task<string?> GetTokenAsync()
        {
            return Task.FromResult(storedToken);
        }

        public Task RemoveTokenAsync()
        {
            storedToken = null;
            return Task.CompletedTask;
        }
    }
}
