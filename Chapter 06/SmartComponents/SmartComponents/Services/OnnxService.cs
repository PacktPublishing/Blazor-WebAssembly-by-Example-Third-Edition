using Microsoft.JSInterop;
using SmartComponents.Models;

namespace SmartComponents.Services
{
    public class OnnxService : IOnnxService, IAsyncDisposable
    {
        private readonly IJSRuntime _js;
        private IJSObjectReference? _module;
        private bool _initialized;

        public OnnxService(IJSRuntime js)
        {
            _js = js;
        }
        public async Task<bool> InitializeAsync()
        {
            if (_initialized) return true;
            _module ??= await _js.InvokeAsync<IJSObjectReference>("import", "./scripts/onnx.js");
            var ok = await _module.InvokeAsync<bool>("initOnnx");
            _initialized = ok;
            return ok;
        }

        public async Task<double[]> RunInferenceAsync(string candidate)
        {
            if (!_initialized) throw new InvalidOperationException("OnnxService not initialized");
            return await _module!.InvokeAsync<double[]>("runInference", candidate);
        }

        public async Task<List<CompareResult>> CompareCandidatesAsync(string query, EmbeddingItem[] candidates)
        {
            if (!_initialized) throw new InvalidOperationException
                ("OnnxService not initialized");
            return await _module!.InvokeAsync<List<CompareResult>>
                ("compareCandidates", query, candidates);
        }

        public async ValueTask DisposeAsync()
        {
            if (_module != null)
            {
                if (_initialized)
                {
                    await _module.InvokeVoidAsync("dispose");
                }
                await _module.DisposeAsync();
                _module = null;
                _initialized = false;
            }
        }


    }
}
