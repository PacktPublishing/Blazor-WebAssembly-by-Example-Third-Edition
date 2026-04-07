namespace Ch11_App.Client.Services
{
    public class TokenStorageService : ITokenStorageService
    {
        private string? _token;

        public Task SetTokenAsync(string token)
        {
            _token = token;
            return Task.CompletedTask;
        }

        public Task<string?> GetTokenAsync()
        {
            return Task.FromResult(_token);
        }

        public Task RemoveTokenAsync()
        {
            _token = null;
            return Task.CompletedTask;
        }
    }
}
