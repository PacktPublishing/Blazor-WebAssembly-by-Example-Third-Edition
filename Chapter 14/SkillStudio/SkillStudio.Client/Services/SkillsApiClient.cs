using SkillStudio.Shared;
using System.Net.Http.Json;

namespace SkillStudio.Client.Services
{
    public class SkillsApiClient(HttpClient _httpClient)
    {
        public async Task<List<SkillDefinition>> GetSkillsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync
               <List<SkillDefinition>>("api/skills");
            return response ?? [];
        }

        public async Task<SkillDefinition?> GetSkillAsync(string name)
        {
            return await _httpClient.GetFromJsonAsync<SkillDefinition>(
                $"api/skills/{name}");
        }

        public async Task<SkillResponse?> RunSkillAsync(SkillRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync(
                "api/skills", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<SkillResponse>();
        }

    }
}
