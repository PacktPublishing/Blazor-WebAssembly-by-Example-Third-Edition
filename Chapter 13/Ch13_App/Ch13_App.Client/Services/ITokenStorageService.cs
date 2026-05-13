namespace Ch13_App.Client.Services
{
    public interface ITokenStorageService
    {
        Task SetTokenAsync(string token);
        Task<string?> GetTokenAsync();
        Task RemoveTokenAsync();
    }
}
