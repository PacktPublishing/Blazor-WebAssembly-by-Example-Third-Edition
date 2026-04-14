using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager.Shared;

namespace TaskManager.Api.Data
{
    public class TaskManagerApiContext : DbContext
    {
        public TaskManagerApiContext (DbContextOptions<TaskManagerApiContext> options)
            : base(options)
        {
        }

        public DbSet<TaskManager.Shared.TaskItem> TaskItem { get; set; } = default!;
    }
}
