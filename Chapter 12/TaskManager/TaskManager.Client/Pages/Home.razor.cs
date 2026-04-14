using Microsoft.AspNetCore.Components;
using TaskManager.Client.Services;
using TaskManager.Shared;

namespace TaskManager.Client.Pages
{
    public partial class Home
    {
        [Inject]
        private TaskItemService taskItemService { get; set; } = default!;

        private IList<TaskItem>? tasks;
        private string? error;
        private int CompletedTaskCount => tasks?.Count(t => t.IsComplete) ?? 0;
        private int TotalTaskCount => tasks?.Count ?? 0;
        private string newTaskName = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                error = null;
                tasks = await taskItemService.GetTasksAsync();
            }
            catch (Exception ex)
            {
                error = $"Failed to load tasks: {ex.Message}";
                tasks = new List<TaskItem>();
            }
        }

        private void ClearError() => error = null;

        private async Task ToggleTaskComplete(TaskItem task)
        {
            try
            {
                error = null;
                task.IsComplete = !task.IsComplete;
                await taskItemService.UpdateTaskAsync(task);
            }
            catch (Exception ex)
            {
                error = $"Failed to update task: {ex.Message}";
                task.IsComplete = !task.IsComplete;
            }
        }
        private async Task DeleteTask(int taskId)
        {
            try
            {
                error = null;
                var success = await taskItemService.DeleteTaskAsync(taskId);
                if (success)
                {
                    var taskToRemove = tasks?.FirstOrDefault(t => t.TaskItemId == taskId);
                    if (taskToRemove != null)
                    {
                        tasks?.Remove(taskToRemove);
                    }
                }
                else
                {
                    error = "Failed to delete task.";
                }
            }
            catch (Exception ex)
            {
                error = $"Failed to delete task: {ex.Message}";
            }
        }
        private async Task AddTask()
        {
            if (string.IsNullOrWhiteSpace(newTaskName))
                return;

            try
            {
                error = null;
                var newTask = await taskItemService.AddTaskAsync(newTaskName.Trim());
                tasks?.Add(newTask);
                newTaskName = string.Empty;
            }
            catch (Exception ex)
            {
                error = $"Failed to add task: {ex.Message}";
            }
        }

    }
}
