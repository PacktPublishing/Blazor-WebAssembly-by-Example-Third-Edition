using SkillStudio.Shared;

namespace SkillStudio.Api.Interfaces
{
    public interface ISkillParser
    {
        SkillDefinition Parse(string filePath);
    }
}
