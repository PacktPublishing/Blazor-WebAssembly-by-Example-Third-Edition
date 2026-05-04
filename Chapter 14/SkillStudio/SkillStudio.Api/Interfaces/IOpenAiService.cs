using SkillStudio.Shared;

namespace SkillStudio.Api.Interfaces
{
    public interface IOpenAiService
    {
        Task<string> RunSkillAsync(SkillDefinition skill, string userInput);
    }
}
