using LearningManagement.IService;
using LearningManagement.Model;
using LearningManagement.Service;


namespace LearningManagement.View;

class LecturerView
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

    public LecturerView(ILoginService loginService, User loggedInUser, IUserService userService,
                        IClassService classService, IFileService fileService, ILearningService learningService,
                        ISessionService sessionService, IMaterialService materialService, IMaterialDtlService materialDtlService,
                        IAssignmentService assignmentService, IForumService forumService)
    {
        this.loginService = loginService;
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

    public void LecturerMenu()
    {
        bool isRunning = true;
        while (isRunning)
        {
            Console.WriteLine($"-{loggedInUser.Id}-");
            Console.WriteLine("\n--- Lecturer Menu ---\n");
            Console.WriteLine("1. Add Learning");
            Console.WriteLine("2. Add Session");
            Console.WriteLine("3. Add Material To Session");
            Console.WriteLine("4. Switch User");
            Console.Write("Choose Option: ");

            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    AddLearning();
                    break;
                case "2":
                    AddSession();
                    break;
                case "3":
                    AddMaterialToSession();
                    break;
                case "4":
                    isRunning = false;
                    break;
                default:
                    Console.WriteLine("Invalid Option");
                    break;
            }
        }
    }

    private void ShowClassesAssignedToLecturer(int lecturerId)
    {
        Console.WriteLine($"Classes assigned to {loggedInUser.Fullname}:");

        List<Class> lecturerClasses = classService.GetClassesAssignedToLecturer(lecturerId);

        if (lecturerClasses != null && lecturerClasses.Count > 0)
        {
            int classNumber = 1;
            foreach (var classLecturer in lecturerClasses)
            {
                Console.WriteLine($"{classNumber}. [{classLecturer.ClassCode}] - {classLecturer.ClassName}");
                classNumber++;
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine("No classes assigned to you.");
        }
    }

    public void AddLearning()
    {
        Console.WriteLine("\n--- Add New Learning ---");

        ShowClassesAssignedToLecturer(loggedInUser.Id);

        Console.Write("Choose a class: ");
        if (int.TryParse(Console.ReadLine(), out int selectedClassIndex))
        {
            List<Class> lecturerClasses = classService.GetClassesAssignedToLecturer(loggedInUser.Id);

            if (selectedClassIndex > 0 && selectedClassIndex <= lecturerClasses.Count)
            {
                Class selectedClass = lecturerClasses[selectedClassIndex - 1];

                Console.WriteLine("Choose learning Day: ");
                ChooseLearningDay();
                Console.WriteLine("Choose a Day: ");

                int CreatedBy = loggedInUser.Id;
                if (int.TryParse(Console.ReadLine(), out int day) && day >= 1 && day <= 5)
                {
                    string dayOfWeek = Enum.GetName(typeof(DayOfWeek), day);

                    Console.Write("Enter Learning Date (yyyy-MM-dd): ");
                    string dateString = Console.ReadLine();

                    if (DateOnly.TryParse(dateString, out DateOnly learningDate))
                    {
                        learningService.AddLearning(selectedClass.Id, dayOfWeek, learningDate, CreatedBy);

                        Console.WriteLine($"Learning in '{selectedClass.ClassName}' on {dayOfWeek} - {learningDate.ToShortDateString()} added successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid date format. Learning not added.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid day. Learning not added.");
                }
            }
            else
            {
                Console.WriteLine("Invalid class selection. Learning not added.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Learning not added.");
        }
    }

    private void ChooseLearningDay()
    {
        Console.WriteLine("1. Monday");
        Console.WriteLine("2. Tuesday");
        Console.WriteLine("3. Wednesday");
        Console.WriteLine("4. Thursday");
        Console.WriteLine("5. Friday");
    }

    public Learning ShowLearningByLecturer(int lecturerId)
    {
        Console.WriteLine($"Learning items for lecturer {loggedInUser.Fullname}:");

        List<Learning> learnings = learningService.GetLearningByLecturer(lecturerId);

        if (learnings != null && learnings.Count > 0)
        {
            int learningNumber = 1;
            foreach (var learning in learnings)
            {
                Console.WriteLine($"{learningNumber}. Day: {learning.DayName} - Learning Date: {learning.LearningDate.ToShortDateString()} - Class: {learning.Class.ClassName}");
                learningNumber++;
            }

            Console.Write("Select a learning item (enter the number): ");
            if (int.TryParse(Console.ReadLine(), out int selectedLearningIndex))
            {
                if (selectedLearningIndex > 0 && selectedLearningIndex <= learnings.Count)
                {
                    return learnings[selectedLearningIndex - 1];
                }
            }
        }
        else
        {
            Console.WriteLine("No learning items found for this lecturer.");
        }

        return null;
    }

    public void AddSession()
    {
        Console.WriteLine("\n--- Add New Session ---");

        Learning selectedLearning = ShowLearningByLecturer(loggedInUser.Id);

        if (selectedLearning != null)
        {
            Console.Write("Enter Session Title: ");
            string sessionTitle = Console.ReadLine();

            Console.Write("Enter Session Start (HH:mm): ");
            TimeSpan sessionStartTime = TimeSpan.Parse(Console.ReadLine());

            Console.Write("Enter Session End (HH:mm): ");
            TimeSpan sessionEndTime = TimeSpan.Parse(Console.ReadLine());

            // Create a Session object
            Session newSession = new Session
            {
                Learning = selectedLearning,
                SessionTitle = sessionTitle,
                SessionStart = sessionStartTime,
                SessionEnd = sessionEndTime,
            };

            int CreatedBy = loggedInUser.Id;
            int newSessionId = sessionService.AddSession(newSession, CreatedBy);

            Console.WriteLine($"Session '{sessionTitle}' added to {selectedLearning.DayName}.");
        }
        else
        {
            Console.WriteLine("Invalid selection. Unable to add session.");
        }
    }

    public void ShowSessionByLecturer(int lecturerId)
    {
        Console.WriteLine($"Sessions for lecturer {loggedInUser.Fullname}:");

        List<Session> sessions = sessionService.GetSessionsByLecturer(lecturerId);

        if (sessions != null && sessions.Count > 0)
        {
            int sessionNumber = 1;
            foreach (var session in sessions)
            {
                Console.WriteLine($"{sessionNumber}. [{session.Id}] - Day: {session.Learning.DayName} - Session Title: {session.SessionTitle} | Start: {session.SessionStart} - End: {session.SessionEnd}");
                sessionNumber++;
            }
        }
        else
        {
            Console.WriteLine("No sessions found for this lecturer.");
        }
    }

    public void AddMaterialToSession()
    {
        Console.WriteLine("\n--- Add Material To Session ---");

        List<Session> sessions = sessionService.GetSessionsByLecturer(loggedInUser.Id);
        if (sessions != null && sessions.Count > 0)
        {
            Console.WriteLine("Available session:");
            for (int i = 0; i < sessions.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Day: {sessions[i].Learning.DayName} - Session: {sessions[i].SessionTitle}");
            }

            // Pilih sesi
            Console.Write("Choose Session: ");
            if (int.TryParse(Console.ReadLine(), out int selectedSessionIndex) && selectedSessionIndex >= 1 && selectedSessionIndex <= sessions.Count)
            {
                Session selectedSession = sessions[selectedSessionIndex - 1];

                if (selectedSession != null && selectedSession.Id > 0)
                {
                    // Kumpulkan informasi materi
                    //Console.Write($"p- {selectedSession.Id} ");
                    Console.Write("Enter Material Title: ");
                    string materialTitle = Console.ReadLine();

                    Console.Write("Enter Material Content: ");
                    string materialContent = Console.ReadLine();

                    // Buat objek Materi
                    Material newMaterial = new Material
                    {
                        MaterialTitle = materialTitle,
                        MaterialContent = materialContent,
                        Session = selectedSession,
                    };

                    // Tambahkan materi ke sesi
                    int CreatedBy = loggedInUser.Id;
                    int newMaterialId = materialService.AddMaterial(newMaterial, CreatedBy);

                    // Periksa apakah materi memiliki file
                    Console.Write("Apakah materi termasuk file? (y/n): ");
                    string response = Console.ReadLine();
                    if (response == "y")
                    {
                        // Kumpulkan detail file
                        Console.Write("Masukkan Judul File: ");
                        string fileTitle = Console.ReadLine();

                        Console.Write("Masukkan Ekstensi File: ");
                        string fileExtension = Console.ReadLine();

                        // Buat objek FileLms
                        FileLms materialFile = new FileLms
                        {
                            FileTitle = fileTitle,
                            FileExtension = fileExtension,
                            CreatedBy = loggedInUser.Id,
                            CreatedAt = DateTime.Now
                        };

                        int newFileId = fileService.CreateFile(fileTitle, fileExtension);

                        MaterialDtl materialDtl = new MaterialDtl
                        {
                            Material = new Material { Id = newMaterialId },
                            MaterialFile = new FileLms { Id = newFileId }
                        };

                        materialDtlService.AddMaterialDetails(materialDtl);

                        Console.WriteLine($"Material '{materialTitle}' has been added to {selectedSession.SessionTitle} session.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid Session Choice.");
                    }
                }
                else
                {
                    Console.WriteLine("No Available session for this lecturer.");
                }
            }
        }
    }

    //    public void AddAssignment()
    //    {
    //        Console.WriteLine("--- Add Assignment to Session ---");

    //        List<Session> sessions = sessionService.GetSessionsByLecturer(loggedInUser.Id);

    //        if (sessions != null && sessions.Count > 0)
    //        {
    //            Console.WriteLine("Available sessions:");
    //            for (int i = 0; i < sessions.Count; i++)
    //            {
    //                Console.WriteLine($"{i + 1}. Day: {sessions[i].LearningId.DayName} - Session: {sessions[i].SessionTitle}");
    //            }

    //            // Choose session
    //            Console.Write("Choose Session: ");
    //            if (int.TryParse(Console.ReadLine(), out int selectedSessionIndex) && selectedSessionIndex >= 1 && selectedSessionIndex <= sessions.Count)
    //            {
    //                Session selectedSession = sessions[selectedSessionIndex - 1];

    //                if (selectedSession != null && selectedSession.Id > 0)
    //                {
    //                    // Gather assignment details
    //                    Console.Write("Enter Assignment Title: ");
    //                    string assignmentTitle = Console.ReadLine();

    //                    Console.Write("Enter Assignment Duration (in minutes): ");
    //                    if (!int.TryParse(Console.ReadLine(), out int assignmentDuration) || assignmentDuration <= 0)
    //                    {
    //                        Console.WriteLine("Invalid duration. Assignment not added.");
    //                        return;
    //                    }

    //                    Assignment newAssignment = new Assignment
    //                    {
    //                        AssignmentTitle = assignmentTitle,
    //                        AssignmentDuration = assignmentDuration,
    //                        SessionId = selectedSession, // Reference to the selected session
    //                        CreatedBy = loggedInUser.Id
    //                    };

    //                    // Save the assignment to the database
    //                    int newAssignmentId = assignmentService.AddAssignment(newAssignment);

    //                    // Determine the type of assignment
    //                    Console.WriteLine("Choose assignment type:");
    //                    Console.WriteLine("1. Essay");
    //                    Console.WriteLine("2. Multiple Choice");
    //                    Console.WriteLine("3. File");
    //                    Console.Write("Enter choice: ");

    //                    if (int.TryParse(Console.ReadLine(), out int assignmentTypeChoice))
    //                    {
    //                        switch (assignmentTypeChoice)
    //                        {
    //                            case 1:
    //                                // Add essay question
    //                                Console.Write("Enter Essay Question Content: ");
    //                                string essayContent = Console.ReadLine();

    //                                Question essayQuestion = new Question
    //                                {
    //                                    QuestionContent = essayContent,
    //                                    QuestionType = "Essay"
    //                                };

    //                                int essayQuestionId = questionService.AddQuestion(essayQuestion);
    //                                assignmentDtlService.AddAssignmentDetail(newAssignmentId, essayQuestionId);
    //                                break;

    //                            case 2:
    //                                // Add multiple-choice questions
    //                                Console.Write("Enter Multiple Choice Question Content: ");
    //                                string mcqContent = Console.ReadLine();

    //                                Question mcqQuestion = new Question
    //                                {
    //                                    QuestionContent = mcqContent,
    //                                    QuestionType = "Multiple Choice"
    //                                };

    //                                int mcqQuestionId = questionService.AddQuestion(mcqQuestion);

    //                                // Add choices
    //                                for (char option = 'A'; option <= 'D'; option++)
    //                                {
    //                                    Console.Write($"Enter Option {option}: ");
    //                                    string optionContent = Console.ReadLine();

    //                                    Console.Write($"Is Option {option} Correct? (y/n): ");
    //                                    bool isCorrect = Console.ReadLine().Equals("y", StringComparison.OrdinalIgnoreCase);

    //                                    QuestionChoice choice = new QuestionChoice
    //                                    {
    //                                        QuestionId = mcqQuestionId,
    //                                        OptionABC = option.ToString(),
    //                                        OptionContent = optionContent,
    //                                        IsCorrect = isCorrect
    //                                    };

    //                                    questionChoiceService.AddQuestionChoice(choice);
    //                                }

    //                                assignmentDtlService.AddAssignmentDetail(newAssignmentId, mcqQuestionId);
    //                                break;

    //                            case 3:
    //                                // Add file-based question
    //                                Console.Write("Enter File Question Content: ");
    //                                string fileContent = Console.ReadLine();

    //                                Question fileQuestion = new Question
    //                                {
    //                                    QuestionContent = fileContent,
    //                                    QuestionType = "File"
    //                                };

    //                                int fileQuestionId = questionService.AddQuestion(fileQuestion);

    //                                Console.Write("Enter File Path: ");
    //                                string filePath = Console.ReadLine();

    //                                FileLms file = new FileLms
    //                                {
    //                                    FilePath = filePath
    //                                };

    //                                int fileId = fileService.CreateFile(file);

    //                                // Link file to the question
    //                                questionFileService.AddQuestionFile(fileId, fileQuestionId);

    //                                assignmentDtlService.AddAssignmentDetail(newAssignmentId, fileQuestionId);
    //                                break;

    //                            default:
    //                                Console.WriteLine("Invalid assignment type choice. Assignment not added.");
    //                                break;
    //                        }

    //                        Console.WriteLine("Assignment added successfully.");
    //                    }
    //                    else
    //                    {
    //                        Console.WriteLine("Invalid assignment type choice. Assignment not added.");
    //                    }
    //                }
    //                else
    //                {
    //                    Console.WriteLine("Invalid session selection. Assignment not added.");
    //                }
    //            }
    //            else
    //            {
    //                Console.WriteLine("Invalid input. Assignment not added.");
    //            }
    //        }
    //        else
    //        {
    //            Console.WriteLine("No sessions found for this lecturer. Assignment not added.");
    //        }
    //    }
}



