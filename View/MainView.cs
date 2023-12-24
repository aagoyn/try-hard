using LearningManagement.DBConfig;
using LearningManagement.Helper;
using LearningManagement.IService;
using LearningManagement.Model;


namespace LearningManagement.View
{
    public class MainView
    {
        private readonly ILoginService loginService;
        private readonly IUserService userService;
        //private readonly IClassService classService;
        private readonly IFileService fileService;
        //private readonly ILearningService learningService;
        //private readonly ISessionService sessionService;
        //private readonly IMaterialService materialService;
        //private readonly IMaterialDtlService materialDtlService;
        //private readonly IAssignmentService assignmentService;
        //private readonly IForumService forumService;
        private readonly SessionHelper sessionHelper;
        private readonly SuperAdminView superAdminView;
        private readonly StudentView studentView;
        private readonly LecturerView lecturerView;


        //private User _user;
        //private int _loggedInUserId;
        private User loggedInUser;

        public MainView(ILoginService loginService, IUserService userService, IClassService classService, IFileService fileService,
                        ILearningService learningService, ISessionService sessionService, IMaterialService materialService,
                        IMaterialDtlService materialDtlService, IAssignmentService assignmentService, IForumService forumService,
                        SessionHelper sessionHelper, User loggedInUser, StudentView studentView, SuperAdminView superAdminView)
        {
            this.loginService = loginService;
            this.userService = userService;
            //this.classService = classService;
            this.fileService = fileService;
            //this.learningService = learningService;
            //this.sessionService = sessionService;
            //this.materialService = materialService;
            //this.assignmentService = assignmentService;
            //this.forumService = forumService;
            //this.materialService = materialService;
            //this.materialDtlService = materialDtlService;
            this.sessionHelper = sessionHelper;
            this.studentView = studentView;
            this.superAdminView = superAdminView;

            this.loggedInUser = loggedInUser;

            //_user = new User();
        }

        //public User LoggedInUser => _loggedInUser;
        public void Welcome()
        {
            Console.WriteLine("Welcome to LMS!\n");
            Console.Write("Do You Have an Account? (y/n): ");

            string response = Console.ReadLine();

            if (response.ToLower() == "n")
            {
                Console.Write("Would you like to register an account? (y/n): ");
                string registerResponse = Console.ReadLine();

                if (registerResponse.ToLower() == "y")
                {
                    RegisterStudent();
                }
                else
                {
                    Console.WriteLine("Thank you for using LMS. Goodbye!");
                }
            }
            else if (response.ToLower() == "y")
            {
                Login();
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter 'y' or 'n'.");
                Welcome();
            }
        }

        public void Login()
        {
            Console.WriteLine("-- Log In to Learning Management System...");

            bool isLoginSuccessful = false;
            while (!isLoginSuccessful)
            {
                Console.WriteLine("--- Welcome to Learning Management System ---");
                Console.Write("Enter email: ");
                string email = Console.ReadLine();
                Console.Write("Enter password: ");
                string password = Console.ReadLine();

                User? userLogin = loginService.Login(email, password);

                if (userLogin != null)
                {
                    loggedInUser.Id = userLogin.Id;
                    loggedInUser.Role = userLogin.Role;

                    //_loggedInUserId = _loggedInUser.Id;
                    sessionHelper.UserId = loggedInUser.Id;
                    sessionHelper.Fullname = userLogin.Fullname;

                    UserLogin(loggedInUser);
                    //isLoginSuccessful = true;
                }
                else
                {
                    Console.WriteLine("Login failed. Incorrect username or password.");
                    Login();
                }
            }
        }

        public void RegisterStudent()
        {
            Console.WriteLine("\n--- Student Register ---");

            Console.Write("Enter Fullname: ");
            string fullName = Console.ReadLine();

            Console.Write("Enter Email: ");
            string email = Console.ReadLine();

            string studentPassword;
            string studentPasswordConfirm;
            do
            {
                Console.Write("Enter Password: ");
                studentPassword = Console.ReadLine();

                Console.Write("Enter Password Confirmation: ");
                studentPasswordConfirm = Console.ReadLine();

                if (studentPassword != studentPasswordConfirm)
                {
                    Console.WriteLine("Password and Password Confirmation do not match. Please try again.");
                }
            } while (studentPassword != studentPasswordConfirm);

            Console.Write("Do you want to add a profile photo? (y/n): ");
            string addPhotoChoice = Console.ReadLine().ToLower();

            int? photoProfileId = null;

            if (addPhotoChoice == "y")
            {
                Console.Write("Enter file content: ");
                string fileTitle = Console.ReadLine();

                Console.Write("Enter file extension: ");
                string fileExtension = Console.ReadLine();

                photoProfileId = fileService.CreateFile(fileTitle, fileExtension);
            }

            userService.RegisterStudent(fullName, email, studentPassword, photoProfileId);

            Console.WriteLine("You've been registered! Continue to log in");

            Login();
        }

        private void UserLogin(User loggedInUser)
        {
            //_loggedInUser = loggedInUser;

            switch (loggedInUser.Role.RoleCode)
            {
                case Constant.Role.SARoleCode:
                    Console.WriteLine($"Login as Super Admin successful.");
                    superAdminView.SuperAdminMenu();
                    break;
                case Constant.Role.StudentRoleCode:
                    Console.WriteLine("Login as Student successful.");
                    studentView.StudentMenu();
                    break;
                case Constant.Role.LecturerRoleCode:
                    Console.WriteLine("Login as Lecturer successful.");
                    lecturerView.LecturerMenu();
                    break;
            }
        }
    }
}
