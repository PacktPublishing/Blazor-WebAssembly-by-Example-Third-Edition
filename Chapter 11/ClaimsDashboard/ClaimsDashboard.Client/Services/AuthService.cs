using ClaimsDashboard.Client.Interfaces;
using ClaimsDashboard.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;

namespace ClaimsDashboard.Client.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient httpClient;
        private readonly ITokenStorageService tokenStorage;
        private readonly AuthenticationStateProvider authenticationStateProvider;

        public AuthService(
            HttpClient httpClient,
            ITokenStorageService tokenStorage,
            AuthenticationStateProvider authenticationStateProvider)
        {
            this.httpClient = httpClient;
            this.tokenStorage = tokenStorage;
            this.authenticationStateProvider = authenticationStateProvider;
        }
        public async Task<bool> LoginAsync(LoginRequest loginRequest)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync(
                    "api/auth/login",
                    loginRequest);

                if (response.IsSuccessStatusCode)
                {
                    var loginResponse = await response.Content
                        .ReadFromJsonAsync<LoginResponse>();

                    if (loginResponse?.Token != null)
                    {
                        await tokenStorage.SetTokenAsync(loginResponse.Token);

                        var authProvider =
                          (CustomAuthenticationStateProvider)authenticationStateProvider;

                        authProvider.NotifyUserAuthentication();

                        return true;
                    }
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
        public async Task LogoutAsync()
        {
            await tokenStorage.RemoveTokenAsync();

            var authProvider =
                (CustomAuthenticationStateProvider)authenticationStateProvider;

            authProvider.NotifyUserLogout();
        }
    }

}
