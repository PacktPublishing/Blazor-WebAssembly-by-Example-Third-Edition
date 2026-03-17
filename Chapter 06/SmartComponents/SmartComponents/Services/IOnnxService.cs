using SmartComponents.Models;

namespace SmartComponents.Services
{
    public interface IOnnxService
    {
        Task<bool> InitializeAsync();
        Task<double[]> RunInferenceAsync(string candidate);
        Task<List<CompareResult>> CompareCandidatesAsync(string query, EmbeddingItem[] candidates);

    }
}
