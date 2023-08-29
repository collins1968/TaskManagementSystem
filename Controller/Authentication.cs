using System.Threading.Tasks;
using TaskManagementSystem.Data;
using TaskManagementSystem.Enums;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Controller
{
    public class AuthenticationController
    {
        TaskManagerContext context = new TaskManagerContext();
        AdminController admin = new AdminController();
        UserController user = new UserController();
         

        //Display main menu
        public void Init()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("*************************");
            Console.WriteLine("* TheJitu Task Manager *");
            Console.WriteLine("************************* \n");

            Console.WriteLine("Select an option");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("1. Register");
            Console.WriteLine("2. Login");
            Console.WriteLine("3. Exit");
            string input = Console.ReadLine();

            MenuRedirect(input);

        }
        //men actions
        public void MenuRedirect(string id)
        {
            UserController user = new UserController();
            switch (id)
            {
                case "1":
                    RegisterUser();
                    break;
                case "2":
                    LoginUser();
                    break;
                case "3":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid input. Please try again");
                    break;
            }
        }

        //Register user
        public void RegisterUser()
        {
            Console.WriteLine("Enter Username ");
            var Username = Console.ReadLine();
            Console.WriteLine("Enter Email ");
            var Email = Console.ReadLine();
            Console.WriteLine("Enter Password ");
            var Password = Console.ReadLine();
            Console.Write("Enter role (User/Admin): ");
            string roleInput = Console.ReadLine();
            if (!Enum.TryParse<UserRole>(roleInput, true, out UserRole role))
            {
                Console.WriteLine("Invalid role. Defaulting to User.");
                role = UserRole.User;
            }
            //create an instance 
            var newUser = new User()
            {
                Username = Username,
                email = Email,
                Password = Password,
                Role = role
            };
            //Add user
            context.Users.Add(newUser);
            //save changes 
            context.SaveChanges();
            LoginUser();
        }

        public void LoginUser()
{
    Console.WriteLine("********");
    Console.WriteLine("LOGIN");
    Console.WriteLine("*********");
    Console.WriteLine("Enter Username:");
    var Username = Console.ReadLine();
    Console.WriteLine("Enter Password:");

    var Password = Console.ReadLine();

    try
    {
        // Check credentials
        var userCred = context.Users.FirstOrDefault(u => u.Username == Username && u.Password == Password);
        if (userCred != null)
        {
            if (userCred.Role == UserRole.Admin)
            {
                Console.WriteLine("Admin login successful.");
                admin.AdminMenu();
            }
            else
            {
                Console.WriteLine("User login successful.");
                user.UserDashboard(userCred.Username, userCred.UserId);
            }
        }
        else
        {
            Console.WriteLine("Invalid username or password.");
                    Init();
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("An error occurred during login: " + ex.Message);
                Init();
    }
}
           
        

    }
}