
using TaskManagementSystem.Enums;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Services
{
    public interface IAdminService
    {
        Task <bool> CreateProject(string name, string description);

        Task<bool>UpdateProject(int id, string name, string description);

        Task<bool>DeleteProject(int id);

        List<Project> GetAllProjects();

        List<TaskDTO> GetAllTasks();

        List<User> GetAllUsers();

        Task<bool> CreateTask(string taskName, string taskDescription, DateTime taskDeadline, TaskPriority taskPriority, taskstatus taskStatus, int userId, int projectId);

        Task<bool> DeleteUser(int id);

        Task<bool> UpdateTask(int id, string taskName, string taskDescription, DateTime taskDeadline, TaskPriority taskPriority, taskstatus taskStatus,  int projectId);
    }
}
