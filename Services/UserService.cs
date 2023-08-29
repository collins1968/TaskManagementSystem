using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Data;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Services
{
    public class UserService : IUserService
    {
        private TaskManagerContext _context = new TaskManagerContext();

        public List<TaskDTO> GetTasksByUserID(int id)
        {
            return _context.Tasks.Where(task => task.UserId == id).ToList();
        }

        public  TaskDTO GetTasksBytaskID(int id)
        {
            return _context.Tasks.FirstOrDefault(task => task.TaskId == id);
        }

        public TaskDTO UpdateTaskStatus(int id, TaskDTO task)
        {
            var taskToUpdate = _context.Tasks.FirstOrDefault(t => t.TaskId == id);
            taskToUpdate.Status = task.Status;
            _context.SaveChanges();
            return taskToUpdate;
        }
    }
}
