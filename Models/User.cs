using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Enums;

namespace TaskManagementSystem.Models
{

    
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string Username { get; set; }

        public string email { get; set; }

        public string Password { get; set; }

        public UserRole Role { get; set; } = UserRole.User; 


        public virtual ICollection<TaskDTO> Tasks { get; set; }
    }
}
