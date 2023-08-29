using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Enums;

namespace TaskManagementSystem.Models
{
    public class TaskDTO
    {
        [Key]
        public int TaskId { get; set; }

        public string Title { get; set; }

        public DateTime? DueDate { get; set; }

        public string Description { get; set; }

        public taskstatus Status { get; set; }

        public TaskPriority Priority { get; set; }

        public int? ProjectId { get; set; }

        public virtual Project Project { get; set; }

        public int? UserId { get; set; }

        public virtual User User { get; set; }
    }
}
