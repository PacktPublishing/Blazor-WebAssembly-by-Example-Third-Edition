using ClaimsDashboard.Client.Interfaces;
using System.Net.Http.Headers;

namespace ClaimsDashboard.Client.Services
{
    public class AuthorizeMessageHandler : DelegatingHandler
    {
        private readonly ITokenStorageService tokenStorage;

        public AuthorizeMessageHandler(ITokenStorageService tokenStorage)
        {
            this.tokenStorage = tokenStorage;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var token = await tokenStorage.GetTokenAsync();

            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
