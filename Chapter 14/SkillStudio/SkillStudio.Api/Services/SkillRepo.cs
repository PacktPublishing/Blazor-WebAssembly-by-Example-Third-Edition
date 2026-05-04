using SkillStudio.Api.Interfaces;
using SkillStudio.Shared;

namespace SkillStudio.Api.Services
{
    public class SkillRepo : ISkillRepo
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ISkillParser _skillParser;
        private readonly Lazy<List<SkillDefinition>> _skills;

        public SkillRepo(IWebHostEnvironment environment, ISkillParser skillParser)
        {
            _environment = environment;
            _skillParser = skillParser;
            _skills = new Lazy<List<SkillDefinition>>(LoadSkills);
        }

        private List<SkillDefinition> LoadSkills()
        {
            var skillsRoot = Path.Combine(_environment.ContentRootPath, "Skills"); 
            var directory = Directory.EnumerateFiles(
                skillsRoot, "SKILL.md", SearchOption.AllDirectories);

            var skills = new List<SkillDefinition>();
            foreach (var file in directory)
            {
                var skill = _skillParser.Parse(file);
                skills.Add(skill);
            }
            return skills.OrderBy(s => s.Name).ToList();
        }

        public IReadOnlyList<SkillDefinition> GetAll() => _skills.Value;

        public SkillDefinition? GetByName(string name) =>  _skills.Value.FirstOrDefault(s => s.Name.Equals(name));

    }

}
