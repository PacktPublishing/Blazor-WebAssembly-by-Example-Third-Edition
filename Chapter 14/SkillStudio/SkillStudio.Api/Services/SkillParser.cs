using SkillStudio.Api.Interfaces;
using SkillStudio.Shared;
using System.Text.RegularExpressions;

namespace SkillStudio.Api.Services
{
    public class SkillParser : ISkillParser
    {
        public SkillDefinition Parse(string filePath)
        {
            var text = File.ReadAllText(filePath);

            var frontMatch = Regex.Match(
                text,
                @"^---\s*(.*?)\s*---",
                RegexOptions.Singleline
            );

            var front = frontMatch.Groups[1].Value.Trim();
            var body = text.Substring(frontMatch.Index + frontMatch.Length).Trim();

            var nameMatch = Regex.Match(
                front,
                @"^name:\s*[\""]?(.+?)[\""]?$",
                RegexOptions.Multiline
            );
            var name = nameMatch.Groups[1].Value.Trim();

            var descMatch = Regex.Match(
                front,
                @"^description:\s*[\""]?(.+?)[\""]?$",
                RegexOptions.Multiline
            );
            var desc = descMatch.Groups[1].Value.Trim();

            return new SkillDefinition(name, desc, body, filePath);
        }
    }
}
