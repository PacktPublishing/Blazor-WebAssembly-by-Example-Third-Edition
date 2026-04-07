using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Ch11_App.Client.Services
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ITokenStorageService tokenStorage;
        private readonly JwtSecurityTokenHandler tokenHandler = new();

        public CustomAuthenticationStateProvider(ITokenStorageService tokenStorage)
        {
            this.tokenStorage = tokenStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await tokenStorage.GetTokenAsync();

            if (!string.IsNullOrWhiteSpace(token))
            {
                var jwtToken = tokenHandler.ReadJwtToken(token);
                var identity = new ClaimsIdentity(jwtToken.Claims, "jwt");
                var user = new ClaimsPrincipal(identity);
                return new AuthenticationState(user);
            }

            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            return new AuthenticationState(anonymousUser);
        }

        public void NotifyUserAuthentication()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public void NotifyUserLogout()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
