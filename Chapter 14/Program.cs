using Microsoft.Extensions.Configuration;
using OpenAI.Chat;
using System.ClientModel;

namespace Ch14_App
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // use the following command to set the API key in user secrets:
            // dotnet user-secrets init
            // dotnet user-secrets set "OpenAI:ApiKey" "<your_api_key>"

            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .Build();

            var apiKey = configuration["OpenAI:ApiKey"]
             ?? throw new InvalidOperationException("Missing OpenAI API key in configuration or environment.");

            while (true)
            {
                Console.WriteLine("\n=== Menu ===");
                Console.WriteLine("1. Simple Chat Completion");
                Console.WriteLine("2. Chat With System Message");
                Console.WriteLine("3. Stream Basic Chat");
                Console.WriteLine("4. Exit");
                Console.Write("\nSelect an option: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await SimpleChatCompletion(apiKey);
                        break;
                    case "2":
                        await ChatWithSystemMessage(apiKey);
                        break;
                    case "3":
                        await StreamBasicChat(apiKey);
                        break;
                    case "4":
                        Console.WriteLine("Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private static async Task SimpleChatCompletion(string apiKey)
        {
            ChatClient client = new("gpt-5.4", apiKey);

            ChatCompletion completion = await client.CompleteChatAsync("Say 'Hello World!'");

            Console.WriteLine(completion.Content[0].Text);
        }

        private static async Task ChatWithSystemMessage(string apiKey)
        {
            ChatClient client = new("gpt-5.4", apiKey);


            ChatCompletion completion = await client.CompleteChatAsync(new ChatMessage[]
            {
        new SystemChatMessage("You are obsessed with dragons."),
        new UserChatMessage("Write a haiku about Blazor."),
            });

            Console.WriteLine(completion.Content[0].Text);
        }

        private static async Task StreamBasicChat(string apiKey)
        {
            ChatClient client = new("gpt-5.4", apiKey);

            ChatCompletionOptions options = new()
            {
                Temperature = 0.7f,
                MaxOutputTokenCount = 100
            };

            var prompt = new ChatMessage[]
            {
                new UserChatMessage("Explain dependency injection in Blazor")
            };

            AsyncCollectionResult<StreamingChatCompletionUpdate> completionUpdates = client.CompleteChatStreamingAsync(prompt, options);

            await foreach (StreamingChatCompletionUpdate completionUpdate in completionUpdates)
            {
                if (completionUpdate.ContentUpdate.Count > 0)
                {
                    Console.Write(completionUpdate.ContentUpdate[0].Text);
                }

                if (completionUpdate.FinishReason == ChatFinishReason.Length)
                {
                    Console.Write(" [truncated due to token limit]");
                }
            }
        }
    }
}
