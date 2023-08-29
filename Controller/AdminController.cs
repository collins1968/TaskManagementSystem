using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Enums;
using TaskManagementSystem.Services;

namespace TaskManagementSystem.Controller
{
    public class AdminController
    {
        AdminService AdminService = new AdminService();
        public void AdminMenu()
        {

            Console.WriteLine("You have successfully Loggedin");
            Console.WriteLine("\n");
            Console.WriteLine("****************************");
            Console.WriteLine($"Welcome to Admin Dashboard");
            Console.WriteLine("****************************");
            Console.WriteLine("Enter your choice");
            Console.WriteLine("1. Create Project");
            Console.WriteLine("2. Update Project");
            Console.WriteLine("3. Delete Project");
            Console.WriteLine("4. View Projects");
            Console.WriteLine("5. Create Task");
            Console.WriteLine("6. Update Task");
            Console.WriteLine("7. View ALl Tasks");
            Console.WriteLine("8. View All Users");
            Console.WriteLine("9. Delete User");
            Console.WriteLine("10. Logout");
            Console.WriteLine("Enter your choice");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    CreateProject();
                    break;
                case 2:
                    UpdateProject();
                    break;
                case 3:
                    DeleteProject();
                    break;
                case 4:
                    ViewProjects1();
                    break;
                case 5:
                    CreateTask();
                    break;
                case 6:
                    UpdateTask();
                    break;
                case 7:
                    ViewAllTasks();
                    break;
                case 8:
                    ViewAllUsers1();
                    break;
                case 9:
                    DeleteUser();
                    break;
                    
                case 10:
                    Logout();
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
             }
        public void CreateUser()
        {
            Console.WriteLine("usercreatd");
        }
        public void ViewUsers()
        {
            Console.WriteLine("user view");
        }
      
        public async Task CreateProject()
        {
            Console.WriteLine("Enter project name: ");
            var projectName = Console.ReadLine();
            Console.WriteLine("Enter project description: ");
            var projectDescription = Console.ReadLine();

        
            bool isCreated = await AdminService.CreateProject(projectName, projectDescription);
            if (isCreated)
            {
                Console.WriteLine("Project created successfully");
                AdminMenu();
            }
            else
            {
                Console.WriteLine("Project creation failed");
                AdminMenu();
            }
        }

        public async Task UpdateProject()
        {
            ViewProjects();
            Console.WriteLine("Enter project id: ");
            int projectId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter project name: ");
            var projectName = Console.ReadLine();
            Console.WriteLine("Enter project description: ");
            var projectDescription = Console.ReadLine();
            bool isUpdated = await AdminService.UpdateProject(projectId, projectName, projectDescription);
            if (isUpdated)
            {
                Console.WriteLine("Project updated successfully");
                AdminMenu();
            }
            else
            {
                Console.WriteLine("Project updation failed");
                AdminMenu();
            }

        }

        public async Task DeleteProject()
        {
            ViewProjects();
            await Console.Out.WriteLineAsync("enter the id of the project:");
            int id = Convert.ToInt32(Console.ReadLine());
            await AdminService.DeleteProject(id);

            Console.WriteLine("Project deleted successfully");
            AdminMenu();
        }

        public async Task ViewProjects()
        {
            var projects = AdminService.GetAllProjects();
            foreach (var project in projects)
            {
                Console.WriteLine($"Id: {project.ProjectId}, Name: {project.Name}, Description: {project.Description}");
            }
        }
        public async Task ViewProjects1()
        {
            var projects = AdminService.GetAllProjects();
            foreach (var project in projects)
            {
                Console.WriteLine($"Id: {project.ProjectId}, Name: {project.Name}, Description: {project.Description}");
            }
            AdminMenu();
        }


        public async Task ViewAllUsers()
        {
            var users = AdminService.GetAllUsers();
            foreach (var user in users)
            {
                Console.WriteLine($"Id: {user.UserId}, Name: {user.Username}, Role: {user.Role}");
            }
            return;
        }

        public async Task ViewAllUsers1()
        {
            var users = AdminService.GetAllUsers();
            foreach (var user in users)
            {
                Console.WriteLine($"Id: {user.UserId}, Name: {user.Username}, Role: {user.Role}");
            }
            AdminMenu();
        }

        public async Task CreateTask()
        {
            ViewProjects();
            Console.WriteLine("Enter project id: ");
            int projectId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter task name: ");
            var taskName = Console.ReadLine();
            Console.WriteLine("Enter task description: ");
            var taskDescription = Console.ReadLine();

            Console.WriteLine("Enter task status:");
            foreach (taskstatus status in Enum.GetValues(typeof(taskstatus)))
            {
                Console.WriteLine($"{(int)status}: {status}");
            }

            taskstatus taskStatus;
            while (true)
            {
                Console.WriteLine("Enter task status (1 for New, 2 for InProgress, 3 for Completed): ");
                if (Enum.TryParse(Console.ReadLine(), out taskStatus))
                {
                    break; // Valid input, exit the loop
                }
                Console.WriteLine("Invalid task status. Please try again.");
            }

            Console.WriteLine("Enter task priority:");
            foreach (TaskPriority priority in Enum.GetValues(typeof(TaskPriority)))
            {
                Console.WriteLine($"{(int)priority}: {priority}");
            }

            TaskPriority taskPriority;
            while (true)
            {
                Console.WriteLine("Enter task priority (1 for Low, 2 for Medium, 3 for High): ");
                if (Enum.TryParse(Console.ReadLine(), out taskPriority))
                {
                    break; // Valid input, exit the loop
                }
                Console.WriteLine("Invalid task priority. Please try again.");
            }

            DateTime dueDate = new DateTime(2023, 12, 31);

            while (true)
            {
                ViewAllUsers();
                Console.WriteLine("Enter user id (or '0' to cancel): ");
                int userId = Convert.ToInt32(Console.ReadLine());

                if (userId == 0)
                {
                    Console.WriteLine("Task creation canceled.");
                    return; // User canceled task creation
                }

                bool isUserAssigned = await AdminService.IsUserAssignedTask(userId);

                if (isUserAssigned)
                {
                    Console.WriteLine("User is already assigned a task. Please pick a different user.");
                }
                else
                {
                    bool isCreated = await AdminService.CreateTask(taskName, taskDescription, dueDate, taskPriority, taskStatus, userId, projectId);

                    if (isCreated)
                    {
                        Console.WriteLine("Task created successfully");
                        AdminMenu();
                    }
                    else
                    {
                        Console.WriteLine("Task creation failed");
                        AdminMenu();
                    }

                    break;
                }
            }
        }
        public async Task UpdateTask()
        
        {
            ViewAllTasks1();
            Console.WriteLine("Enter task id: ");
            int taskId = Convert.ToInt32(Console.ReadLine());
            ViewAllUsers();
            //Console.WriteLine("Enter user id: ");
            //int userId = Convert.ToInt32(Console.ReadLine());
            //bool isUserAssigned = await AdminService.IsUserAssignedTask(userId);
            //if (isUserAssigned)
            //{
            //    Console.WriteLine("User is already assigned a task. Please pick a different user.");
            //    AdminMenu();
            //}
            //else
            //{
                ViewProjects();
                Console.WriteLine("Enter project id: ");
                int projectId = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter task name: ");
                var taskName = Console.ReadLine();
                Console.WriteLine("Enter task description: ");
                var taskDescription = Console.ReadLine();

                Console.WriteLine("Enter task status:");
                foreach (taskstatus status in Enum.GetValues(typeof(taskstatus)))
                {
                    Console.WriteLine($"{(int)status}: {status}");
                }

                taskstatus taskStatus;
                while (true)
                {
                    Console.WriteLine("Enter task status (1 for New, 2 for InProgress, 3 for Completed): ");
                    if (Enum.TryParse(Console.ReadLine(), out taskStatus))
                    {
                        break; // Valid input, exit the loop
                    }
                    Console.WriteLine("Invalid task status. Please try again.");
                }

                Console.WriteLine("Enter task priority:");
                foreach (TaskPriority priority in Enum.GetValues(typeof(TaskPriority)))
                {
                    Console.WriteLine($"{(int)priority}: {priority}");
                }

                TaskPriority taskPriority;
                while (true)
                {
                    Console.WriteLine("Enter task priority (1 for Low, 2 for Medium, 3 for High): ");
                    if (Enum.TryParse(Console.ReadLine(), out taskPriority))
                    {
                        break; // Valid input, exit the loop
                    }
                    Console.WriteLine("Invalid task priority. Please try again.");
                }

                DateTime dueDate = new DateTime(2023, 12, 31);

                bool isUpdated = await AdminService.UpdateTask(taskId, taskName, taskDescription, dueDate, taskPriority, taskStatus, projectId);

                if (isUpdated)
                {
                    Console.WriteLine("Task updated successfully");

                    AdminMenu();
                }
                else
                {
                    Console.WriteLine("Task updation failed");
                    AdminMenu();
                }
            
        }

        public async Task ViewAllTasks1()
        {
            var tasks = AdminService.GetAllTasks();
            foreach (var task in tasks)
            {
                Console.WriteLine($"Id: {task.TaskId}, Name: {task.Title}, Description: {task.Description}, Due Date: {task.DueDate}, Priority: {task.Priority}, Status: {task.Status}, User Id: {task.UserId}, Project Id: {task.ProjectId}");
            }
        }
        public async Task ViewAllTasks()
        {
            var tasks = AdminService.GetAllTasks();
            foreach (var task in tasks)
            {
                Console.WriteLine($"Id: {task.TaskId}, Name: {task.Title}, Description: {task.Description}, Due Date: {task.DueDate}, Priority: {task.Priority}, Status: {task.Status}, User Id: {task.UserId}, Project Id: {task.ProjectId}");
            }
            AdminMenu();
        }
        public async void DeleteUser()
        {
            ViewAllUsers();
            Console.WriteLine("Enter user id: ");
            int userId = Convert.ToInt32(Console.ReadLine());
            bool isDeleted = await AdminService.DeleteUser(userId);
            if (isDeleted)
            {
                Console.WriteLine("User deleted successfully");
                AdminMenu();
            }
            else
            {
                Console.WriteLine("User deletion failed");
                AdminMenu();
            }
        }
        public void Logout()
        {
            Console.WriteLine("Logged out successfully");
            return;
        }
    }
}
