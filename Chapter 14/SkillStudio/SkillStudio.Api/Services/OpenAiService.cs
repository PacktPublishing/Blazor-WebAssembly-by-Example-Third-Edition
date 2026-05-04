using OpenAI.Chat;
using SkillStudio.Api.Interfaces;
using SkillStudio.Shared;

namespace SkillStudio.Api.Services
{
    public class OpenAiService(ChatClient _chatClient) : IOpenAiService
    {
        public async Task<string> RunSkillAsync(SkillDefinition skill, string userInput)
        {
            var systemPrompt =
                $"""
                You are executing the skill "{skill.Name}".

                Skill description:
                {skill.Description}

                Skill instructions:
                {skill.Instructions}

                IMPORTANT: Structure your response in two sections:

                1. First, show your visible explanation under a "## Approach" heading. Explain:
                    - Your interpretation of the request
                    - The main factors you considered
                    - Any notable creative decisions
                    - Provide a visual representation of your thought process

                2. Then provide the final output under a "## Result" heading.
                

                Use markdown formatting.
                """;

            var messages = new ChatMessage[]
            {
                new SystemChatMessage(systemPrompt),
                new UserChatMessage(userInput)
            };

            ChatCompletionOptions options = new()
            {
                Temperature = 0.7f
            };

            var completion = await _chatClient.CompleteChatAsync(messages, options);

            return completion.Value.Content[0].Text;
        }

    }
}
