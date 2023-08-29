using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Data;
using TaskManagementSystem.Enums;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Services
{
  public class AdminService : IAdminService
    {
    private  TaskManagerContext _context = new TaskManagerContext();
    public async Task<bool> CreateProject(string name, string description)
    {
            try
            {
                var project = new Project()
                {
                    Name = name,
                    Description = description
                };

                _context.Projects.Add(project);
                _context.SaveChanges();
                return true; 

            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"failed to create project: {ex}");
                return false; 
            }
        }

      public async Task<bool> UpdateProject(int id, string name, string description)
        {
            try
            {
                var project = _context.Projects.Find(id);
                project.Name = name;
                project.Description = description;
                _context.SaveChanges();
                return true; 
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"failed to update project: {ex}");
                return false; 
            }
        }
       
       public async Task<bool> DeleteProject(int id)
        {
            try
            {
                var project = _context.Projects.Find(id);
                _context.Projects.Remove(project);
                _context.SaveChanges();
                return true; 
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"failed to delete project: {ex}");
                return false; 
            }
        }

        public List<Project> GetAllProjects()
        {
            return _context.Projects.ToList();

        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }
        public async Task<bool> IsUserAssignedTask(int userId)
        {
            List<TaskDTO> tasks = GetTasks(); 

            bool isAssigned = tasks.Any(task => task.UserId == userId);

            return isAssigned;
        }

        private List<TaskDTO> GetTasks()
        {
            return _context.Tasks.ToList();

        }

        public async Task<bool> CreateTask(string taskName, string taskDescription, DateTime taskDeadline, TaskPriority taskPriority, taskstatus taskStatus, int userId, int projectId)
        {
            try
            {
                var task = new TaskDTO()
                {
                    Title = taskName,
                    Description = taskDescription,
                    DueDate = taskDeadline,
                    Priority = taskPriority,
                    Status = taskStatus,
                    UserId = userId,
                    ProjectId = projectId
                };

                _context.Tasks.Add(task);
                _context.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"Failed to create task: {ex}");
                return false;
            }
        }

        public async Task<bool> UpdateTask(int id, string taskName, string taskDescription, DateTime taskDeadline, TaskPriority taskPriority, taskstatus taskStatus, int projectId)
        {
            try
            {
                var task = _context.Tasks.Find(id);
                task.Title = taskName;
                task.Description = taskDescription;
                task.DueDate = taskDeadline;
                task.Priority = taskPriority;
                task.Status = taskStatus;
                //task.UserId = userId;
                task.ProjectId = projectId;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"failed to update task: {ex}");
                return false;
            }
        }

        public async Task<bool> DeleteTask(int id)
        {
            try
            {
                var task = _context.Tasks.Find(id);
                _context.Tasks.Remove(task);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"failed to delete task: {ex}");
                return false;
            }
        }

        public List<TaskDTO> GetAllTasks()
        {
            return _context.Tasks.ToList();
        }

        public List<TaskDTO> GetAllTasksByProjectId(int projectId)
        {
            return _context.Tasks.Where(task => task.ProjectId == projectId).ToList();
        }

        public async Task <bool> DeleteUser(int id)
        {
            try
            {
                var user = _context.Users.Find(id);
                _context.Users.Remove(user);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"failed to delete user: {ex}");
                return false;
            }
            
        }

       

    }
}
