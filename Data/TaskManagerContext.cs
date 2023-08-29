

using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Models;


namespace TaskManagementSystem.Data
{
    public class TaskManagerContext: DbContext
    {
    
        public DbSet<TaskDTO> Tasks { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Project> Projects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=FRUIT;Database=TaskManagerJitu;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }
}
