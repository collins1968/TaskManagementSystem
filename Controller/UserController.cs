using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Data;
using TaskManagementSystem.Enums;
using TaskManagementSystem.Models;
using TaskManagementSystem.Services;

namespace TaskManagementSystem.Controller
{
    public class UserController
    {
        private TaskManagerContext context = new TaskManagerContext();
        UserService userService = new UserService();
      

        //view user dashboard
        public void UserDashboard(string username, int id)
        {
            Console.WriteLine("You have successfully Loggedin");
            Console.WriteLine("\n");
            Console.WriteLine("*************************************");
            Console.WriteLine($"Welcome {username} to User Dashboard");
            Console.WriteLine("*************************************");
            Console.WriteLine("Select an option");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("1. View Tasks");
           
            string input = Console.ReadLine();
            if (input == "1")
            {
                ViewUserTasks(id);
            }
        }
    

        //View tasks assigned to user
        public void ViewUserTasks(int Id)
        {
            List<TaskDTO> ProjectTasks = userService.GetTasksByUserID(Id);
            if (ProjectTasks == null)
            {
                Console.WriteLine("You have no tasks assigned!");
            }
            else
            {
                foreach (var task in ProjectTasks)
                {
                    Console.WriteLine(
                        $"Project Task ID: {task.TaskId} |, Description: {task.Description}\n" +
                        $"Project Status: {task.Status} \n" +
                        $"Project Priority: {task.Priority} \n" +
                        $"Project DueDate: {task.DueDate}");
                }
                UpdateTasksStatus(Id);
            }
        }

        //Change task status - open or inProgress or Completed
        public void UpdateTasksStatus(int Id)
        {
            Console.WriteLine("Enter task ID to update status: ");
            if (int.TryParse(Console.ReadLine(), out int taskId))
            {
                Console.WriteLine("Select new task progress: ");
                foreach (taskstatus progress in Enum.GetValues(typeof(taskstatus)))
                {
                    Console.WriteLine($"{(int)progress}. {progress}");
                }
                if (Enum.TryParse(Console.ReadLine(), out taskstatus newProgress))
                {
                    //check if the task belongs to user before updating progress
                    TaskDTO projectTask = userService.GetTasksBytaskID(taskId);
                    if (projectTask != null && projectTask.UserId == Id)
                    {
                        userService.UpdateTaskStatus(taskId, new TaskDTO { Status = newProgress });
                        Console.WriteLine("Task progress updated successfully!");
                        
                    }
                    else
                    {
                        Console.WriteLine("Task not found or does not belong to the same user.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input..");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }
        }
    }
}