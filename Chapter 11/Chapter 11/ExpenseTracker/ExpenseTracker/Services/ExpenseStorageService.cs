using ExpenseTracker.Models;
using Microsoft.JSInterop;

namespace ExpenseTracker.Services
{
    public class ExpenseStorageService : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;

        public ExpenseStorageService(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./scripts/expenseTrackerDB.js").AsTask());
        }
        public async Task<List<Expense>> GetExpensesAsync()
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<List<Expense>>("getExpenses") ?? new();
        }

        public async Task<Expense?> GetExpenseAsync(string id)
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<Expense?>("getExpense", id);
        }

        public async Task<Expense> SaveExpenseAsync(Expense expense)
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<Expense>("saveExpense", expense);
        }

        public async Task DeleteExpenseAsync(string id)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("deleteExpense", id);
        }

        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }
        }

    }
}
