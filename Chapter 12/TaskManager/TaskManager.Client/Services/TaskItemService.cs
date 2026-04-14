using System.Net.Http.Json;
using TaskManager.Shared;

namespace TaskManager.Client.Services
{
    public class TaskItemService(HttpClient httpClient)
    {
        public async Task<IList<TaskItem>> GetTasksAsync()
        {
            var response = await httpClient
                .GetFromJsonAsync<IList<TaskItem>>("api/TaskItems");

            return response ?? new List<TaskItem>();
        }
        public async Task<TaskItem> AddTaskAsync(string taskName)
        {
            var newTask = new TaskItem { TaskName = taskName };

            var response = await httpClient.PostAsJsonAsync("api/TaskItems", newTask);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<TaskItem>()
                ?? throw new InvalidOperationException(
                   "Failed to deserialize created task.");
        }
        public async Task UpdateTaskAsync(TaskItem task)
        {
            var response = await httpClient.PutAsJsonAsync($"api/TaskItems/{task.TaskItemId}", task);
            response.EnsureSuccessStatusCode();
        }
        public async Task<bool> DeleteTaskAsync(int id)
        {
            var response = await httpClient.DeleteAsync($"api/TaskItems/{id}");
            return response.IsSuccessStatusCode;
        }

    }
}
