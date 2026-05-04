namespace SkillStudio.Shared
{
public record SkillResponse(
    string SkillName, 
    string? Result, 
    DateTimeOffset CompletedAt);
}
