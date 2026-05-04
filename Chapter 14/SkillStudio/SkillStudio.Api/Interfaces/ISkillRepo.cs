using SkillStudio.Shared;

namespace SkillStudio.Api.Interfaces
{
    public interface ISkillRepo
    {
        IReadOnlyList<SkillDefinition> GetAll();
        SkillDefinition? GetByName(string name);

    }
}
