using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TaskManager.Shared
{
    public class TaskItem
    {
        public int TaskItemId { get; set; }
        [Required]
        [StringLength(100)]

        public string TaskName { get; set; } = string.Empty;
        public bool IsComplete { get; set; }

    }
}
