//using LearningManagement.IService;
//using LearningManagement.Model;
//using LearningManagement.Service;
//using LearningManagement.View;


//namespace LearningManagement.View;


//public class RegisterView
//{
//    private readonly IUserService userService;
//    private readonly ILoginService loginService;
//    private readonly IClassService classService;
//    private readonly IFileService fileService;
//    private readonly ILearningService learningService;
//    private readonly ISessionService sessionService;
//    private readonly IMaterialService materialService;
//    private readonly IMaterialDtlService materialDtlService;
//    private readonly IClassEnrollmentService classEnrollmentService;
//    private readonly IAssignmentService assignmentService;
//    private readonly IForumService forumService;
//    private readonly MainView mainView;

//    private User loggedInUser;

//    public RegisterView(IUserService userService, ILoginService loginService, User loggedInUser,
//                        IClassService classService, IFileService fileService, ILearningService learningService,
//                        ISessionService sessionService, IMaterialService materialService, IMaterialDtlService materialDtlService,
//                        IClassEnrollmentService classEnrollmentService, IAssignmentService assignmentService, IForumService forumService,
//                        MainView mainView)
//    {
//        this.userService = userService;
//        this.loginService = loginService;
//        this.classService = classService;
//        this.fileService = fileService;
//        this.learningService = learningService;
//        this.sessionService = sessionService;
//        this.materialService = materialService;
//        this.materialDtlService = materialDtlService;
//        this.classEnrollmentService = classEnrollmentService;
//        this.assignmentService = assignmentService;
//        this.forumService = forumService;
//        this.mainView = mainView;
//    }

//    public void RegisterStudent()
//    {
//        Console.WriteLine("\n--- Student Register ---");
//        //Console.WriteLine($"-{loggedInUser.Id}-");

//        Console.Write("Enter Fullname: ");
//        string fullName = Console.ReadLine();

//        Console.Write("Enter Email: ");
//        string email = Console.ReadLine();

//        string studentPassword;
//        string studentPasswordConfirm;
//        do
//        {
//            Console.Write("Enter Password: ");
//            studentPassword = Console.ReadLine();

//            Console.Write("Enter Password Confirmation: ");
//            studentPasswordConfirm = Console.ReadLine();

//            if (studentPassword != studentPasswordConfirm)
//            {
//                Console.WriteLine("Password and Password Confirmation do not match. Please try again.");
//            }
//        } while (studentPassword != studentPasswordConfirm);


//        userService.RegisterStudent(fullName, email, studentPassword);

//        Console.WriteLine("You've been registered! Continue to log in");
//      }
//}


//        //                                 materialService, materialDtlService, classEnrollmentService, assignmentService, forumService);
//        //mainView.Login();
//        //MainView mainView = new MainView(loginService, userService, classService, fileService, learningService, sessionService,