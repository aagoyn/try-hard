using LearningManagement.IService;
using LearningManagement.Model;

namespace LearningManagement.View;

public class SuperAdminView
{
    private readonly ILoginService loginService;
    private readonly IUserService userService;
    private readonly IClassService classService;
    private readonly IFileService fileService;
    private readonly ILearningService learningService;
    private readonly ISessionService sessionService;
    private readonly IMaterialService materialService;
    private readonly IMaterialDtlService materialDtlService;
    private readonly IAssignmentService assignmentService;
    private readonly IForumService forumService;


    private User loggedInUser;

    public SuperAdminView(ILoginService loginService, User loggedInUser, IUserService userService,
                            IClassService classService, IFileService fileService, ILearningService learningService,
                            ISessionService sessionService, IMaterialService materialService, IMaterialDtlService materialDtlService)
    {
        this.loggedInUser = loggedInUser;
        this.userService = userService;
        this.classService = classService;
        this.fileService = fileService;
        this.learningService = learningService;
        this.sessionService = sessionService;
        this.materialService = materialService;
        this.materialDtlService = materialDtlService;
        this.assignmentService = assignmentService;
        this.forumService = forumService;
    }

    public void SuperAdminMenu()
    {
        bool isRunning = true;
        while (isRunning)
        {
            Console.WriteLine($"-{loggedInUser.Id}-");
            Console.WriteLine("\n--- Menu Super Admin ---\n");
            Console.WriteLine("1. Register Lecturer");
            Console.WriteLine("2. Register Class");
            Console.WriteLine("3. Switch User");
            Console.Write("Choose Option: ");

            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    RegisterLecturer();
                    break;
                case "2":
                    AddClass();
                    break;
                case "3":
                    isRunning = false; ;
                    break;
                default:
                    Console.WriteLine("Invalid Option");
                    break;
            }
        }
    }

    public void RegisterLecturer()
    {

        Console.WriteLine("--- Register Lecturer ---");

        Console.Write("Input Fullname: ");
        string fullname = Console.ReadLine();

        Console.Write("Input Email: ");
        string email = Console.ReadLine();

        int createdBy = loggedInUser.Id;

        userService.RegisterLecturer(fullname, email, createdBy);

        Console.WriteLine($"Lecturer {fullname} with email [{email}] has been added!");
    }


    private void AddClass()
    {
        Console.WriteLine("--- Add New Class ---");

        Console.Write("Enter Class Name: ");
        string className = Console.ReadLine();

        Console.Write("Enter Class Code: ");
        string classCode = Console.ReadLine();

        Console.Write("Enter Class Description: ");
        string classDesc = Console.ReadLine();

        Console.Write("Enter Class Image: ");
        string classImageTitle = Console.ReadLine();

        Console.Write("Enter Class Image Extension: ");
        string classImageExtension = Console.ReadLine();

        FileLms file = new FileLms
        {
            FileTitle = classImageTitle,
            FileExtension = classImageExtension,
            CreatedBy = loggedInUser.Id,
            CreatedAt = DateTime.Now
        };

        int newFileId = fileService.CreateFile(classImageTitle, classImageExtension);

        Class newClass = new Class
        {
            ClassName = className,
            ClassCode = classCode,
            ClassDesc = classDesc,
            ClassPhoto = new FileLms { Id = newFileId }
        };

        int selectedLecturerIndex;
        ShowLecturerList();
        Console.Write("Choose lecturer to get assigned to this class: ");

        if (int.TryParse(Console.ReadLine(), out selectedLecturerIndex))
        {
            List<User> lecturers = userService.GetAllLecturers();

            if (selectedLecturerIndex > 0 && selectedLecturerIndex <= lecturers.Count)
            {
                int selectedLecturerId = lecturers[selectedLecturerIndex - 1].Id;

                int CreatedBy = loggedInUser.Id;
                int newClassId = classService.AddClass(newClass, selectedLecturerId);

                Console.WriteLine($"Class {className} - code [{classCode}] has been added!");

                classService.AssignLecturerToClass(selectedLecturerId, newClassId);

                Console.WriteLine($"Lecturer {lecturers[selectedLecturerIndex - 1].Fullname} has been assigned to class {newClass.ClassName}.");
            }
            else
            {
                Console.WriteLine("Invalid selection. No lecturer assigned to the class.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input. No lecturer assigned to the class.");
        }
    }

    private void ShowLecturerList()
    {
        Console.WriteLine("\nAvailable lecturers:");

        List<User> lecturers = userService.GetAllLecturers();

        if (lecturers != null && lecturers.Count > 0)
        {
            int lecturerNumber = 1;
            foreach (var lecturer in lecturers)
            {
                Console.WriteLine($"{lecturerNumber}. {lecturer.Fullname}");
                lecturerNumber++;
            }
        }
        else
        {
            Console.WriteLine("No Available Lecturer");
        }
    }
}

