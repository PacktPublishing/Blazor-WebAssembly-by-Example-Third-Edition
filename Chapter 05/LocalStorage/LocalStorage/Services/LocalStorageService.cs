using Microsoft.JSInterop;
using System.Text.Json;

namespace LocalStorage.Services
{
    public class LocalStorageService : ILocalStorageService, IAsyncDisposable
    {
        private readonly IJSRuntime js;
        private IJSObjectReference? module;

        public LocalStorageService(IJSRuntime jsRuntime)
        {
            js = jsRuntime;
        }

        private async Task EnsureModuleAsync()
        {
            if (module is null)
            {
                module = await js.InvokeAsync<IJSObjectReference>(
                    "import", "./scripts/jsInterop.js");
            }
        }

        public async ValueTask SetItemAsync<T>(string key, T item)
        {
            await EnsureModuleAsync();
            await module!.InvokeVoidAsync("setLocalStorage", key, JsonSerializer.Serialize(item));
        }

        public async ValueTask<T?> GetItemAsync<T>(string key)
        {
            await EnsureModuleAsync();
            var json = await module!.InvokeAsync<string>("getLocalStorage", key);
            return JsonSerializer.Deserialize<T>(json);
        }

        public async ValueTask DisposeAsync()
        {
            if (module is not null)
            {
                await module.DisposeAsync();
            }
        }
    }
}