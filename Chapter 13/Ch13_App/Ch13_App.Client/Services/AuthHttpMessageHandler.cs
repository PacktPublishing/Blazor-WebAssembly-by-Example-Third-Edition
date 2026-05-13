using System.Net.Http.Headers;

namespace Ch13_App.Client.Services
{
    public class AuthHttpMessageHandler : DelegatingHandler
    {
        private readonly ITokenStorageService tokenStorage;

        public AuthHttpMessageHandler(ITokenStorageService tokenStorage)
        {
            this.tokenStorage = tokenStorage;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await tokenStorage.GetTokenAsync();

            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
