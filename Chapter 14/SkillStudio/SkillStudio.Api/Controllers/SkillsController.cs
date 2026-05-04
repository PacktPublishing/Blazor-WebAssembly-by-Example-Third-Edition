using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkillStudio.Api.Interfaces;
using SkillStudio.Shared;

namespace SkillStudio.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController(ISkillRepo _skillRepo, IOpenAiService _openAiService) : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<object>> GetSkills()
        {
            var skills = _skillRepo.GetAll()
                .Select(s => new
                {
                    s.Name,
                    s.Description
                });

            return Ok(skills);
        }

        [HttpGet("{name}")]
        public ActionResult<SkillDefinition> GetSkill(string name)
        {
            var skill = _skillRepo.GetByName(name);

            if (skill is null)
            {
                return NotFound();
            }

            return Ok(skill);
        }

        [HttpPost]
        public async Task<ActionResult<SkillResponse>> RunSkill([FromBody] SkillRequest request)
        {
            var skill = _skillRepo.GetByName(request.SkillName);
            var result = await _openAiService.RunSkillAsync(skill, request.UserInput);
            var response = new SkillResponse(skill.Name, result, DateTimeOffset.UtcNow);
            return Ok(response);
        }

    }
}
