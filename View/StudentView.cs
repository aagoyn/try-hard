using LearningManagement.Constant;
using LearningManagement.Helper;
using LearningManagement.IService;
using LearningManagement.Model;
using LearningManagement.Service;

namespace LearningManagement.View;

public class StudentView
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
    private readonly ISubmissionService submissionService;
    private readonly SessionHelper sessionHelper;


    private User loggedInUser;

    public StudentView(ILoginService loginService, User loggedInUser, IUserService userService,
                    IClassService classService, IFileService fileService, ILearningService learningService,
                    ISessionService sessionService, IMaterialService materialService, IMaterialDtlService materialDtlService,
                    IAssignmentService assignmentService, IForumService forumService,
                    SessionHelper sessionHelper, ISubmissionService submissionService)
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
        this.submissionService = submissionService;
        this.sessionHelper = sessionHelper;
    }
    public void StudentMenu()
    {
        bool isRunning = true;
        while (isRunning)
        {
            Console.WriteLine("\n--- Student Menu ---");
            Console.WriteLine("------------------------");
            Console.WriteLine($"  --WELCOME {sessionHelper.Fullname}!--\n");
            Console.WriteLine("1. Enroll To Class");
            Console.WriteLine("2. My Classes");
            Console.WriteLine("3. Switch User");
            //Console.WriteLine("4. Switch User");
            Console.Write("Choose Option: ");

            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    EnrollClass();
                    break;
                case "2":
                    ShowEnrolledClasses();
                    break;
                case "3":
                    isRunning = false;
                    break;
                //case "4":
                //    break;
                default:
                    Console.WriteLine("Invalid Option");
                    break;
            }
        }
    }

    public void EnrollClass()
    {
        do
        {
            Console.WriteLine("--- Enroll to Class ---");

            var availableClasses = classService.GetUnenrolledClasses(loggedInUser.Id);
            if (availableClasses.Count == 0)
            {
                Console.WriteLine("No available classes for enrollment.");
                break;
            }

            Console.WriteLine("Available Classes:");
            for (int i = 0; i < availableClasses.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {availableClasses[i].ClassCode} - {availableClasses[i].ClassName}");
            }

            Console.Write("Choose a Class to Enroll : ");
            if (int.TryParse(Console.ReadLine(), out int selectedClassIndex) && selectedClassIndex >= 1 && selectedClassIndex <= availableClasses.Count)
            {
                Class selectedClass = availableClasses[selectedClassIndex - 1];
                var enrollStudent = classService.EnrollStudent(selectedClass.Id, loggedInUser.Id);

                if (enrollStudent > 0)
                {
                    Console.WriteLine($"Hallo, {sessionHelper.Fullname}. You have successfully enrolled in class: [{selectedClass.ClassCode} - {selectedClass.ClassName}]");
                }
                else
                {
                    Console.WriteLine("Enrollment failed. Please try again.");

                }

                Console.Write("Do you want to enroll in another class? (y/n): ");
                if (Console.ReadLine().ToLower() == "n")
                {
                    break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Enrollment canceled.");
                break;
            }
        } while (true);
    }

    public void ShowEnrolledClasses()
    {
        Console.WriteLine($"\n{sessionHelper.Fullname} enrolled classes\n");

        var enrolledClasses = classService.GetEnrolledClasses(loggedInUser.Id);

        if (enrolledClasses != null && enrolledClasses.Count > 0)
        {
            int classNumber = 1;
            foreach (var enrolledClass in enrolledClasses)
            {
                Console.WriteLine($"{classNumber}. {enrolledClass.ClassCode} - {enrolledClass.ClassName}");
                classNumber++;
            }

            Console.Write("Choose a Class to view Learning items: ");
            if (int.TryParse(Console.ReadLine(), out int selectedClassIndex) && selectedClassIndex >= 1 && selectedClassIndex <= enrolledClasses.Count)
            {
                Class selectedClass = enrolledClasses[selectedClassIndex - 1];

                ShowLearningByClass(selectedClass.Id);
            }
            else
            {
                Console.WriteLine("Invalid input. Returning to the main menu.");
            }
        }
        else
        {
            Console.WriteLine("No enrolled classes found.");
        }
    }

    public void ShowLearningByClass(int classId)
    {
        Class selectedClass = classService.GetClassById(classId);

        if (selectedClass != null)
        {
            Console.WriteLine($"\n- Learning items for Class: {selectedClass.ClassName} ({selectedClass.ClassCode})");

            List<Learning> learnings = learningService.GetLearningByClass(classId);

            if (learnings != null && learnings.Count > 0)
            {
                int learningNumber = 1;
                foreach (var learning in learnings)
                {
                    Console.WriteLine($"{learningNumber}. Day: {learning.DayName} - Learning Date: {learning.LearningDate.ToShortDateString()}");
                    learningNumber++;
                }

                Console.Write("Choose a Learning item to view Sessions: ");
                if (int.TryParse(Console.ReadLine(), out int selectedLearningIndex) && selectedLearningIndex >= 1 && selectedLearningIndex <= learnings.Count)
                {
                    Learning selectedLearning = learnings[selectedLearningIndex - 1];

                    ShowSessions(selectedLearning.Id);
                }
                else
                {
                    Console.WriteLine("Invalid input. Returning to the main menu.");
                }
            }
            else
            {
                Console.WriteLine("\tNo learning items found for this class.\n");
            }
        }
        else
        {
            Console.WriteLine("Class not found.");
        }
    }

    public void ShowSessions(int learningId)
    {
        List<Session> sessions = sessionService.GetSessionsByLearning(learningId);

        if (sessions != null && sessions.Count > 0)
        {
            Console.WriteLine($" \n- Sessions for the selected Learning item:");

            int sessionNumber = 1;
            foreach (var session in sessions)
            {
                var attendance = sessionService.GetSessionAttendanceStatus(session.Id);

                if (attendance == null)
                {
                    Console.WriteLine($"{session.SessionTitle} | Start: {session.SessionStart} - End: {session.SessionEnd}");
                    Console.WriteLine("1. Request Attendance");
                    Console.Write("Choose an option: ");

                    int userChoice;
                    if (int.TryParse(Console.ReadLine(), out userChoice) && userChoice == 1)
                    {
                        sessionService.RequestAttendance(session.Id);

                        Console.WriteLine("\nAttendance request submitted successfully!");
                        Console.WriteLine("Please wait for Teacher approval to be able to view " + session.SessionTitle + " content");
                    }
                    else
                    {
                        Console.WriteLine("Invalid option. Returning to the main menu.");
                    }
                }
                else if (!attendance.IsApprove)
                {
                    Console.WriteLine($"\n{session.SessionTitle} ({session.SessionStart} - {session.SessionEnd})");
                    Console.WriteLine("Attendance request is pending approval. You cannot access the session at the moment.");
                }
                else
                {
                    Console.WriteLine($"{sessionNumber}. {session.SessionTitle} | Start: {session.SessionStart} - End: {session.SessionEnd}");
                    sessionNumber++;
                }
            }

            if (sessionNumber > 1)
            {
                Console.Write("Choose a Session (or 0 to go back): ");
                int sessionOption;
                if (int.TryParse(Console.ReadLine(), out sessionOption))
                {
                    if (sessionOption == 0)
                    {
                        return;
                    }

                    if (sessionOption > 0 && sessionOption < sessionNumber)
                    {
                        var selectedSession = sessions[sessionOption - 1];
                        Console.WriteLine($"You selected Session {selectedSession.SessionTitle}");

                        var forums = forumService.GetForum(selectedSession.Id);
                        var materials = materialService.GetMaterialBySession(selectedSession.Id);
                        var assignments = assignmentService.GetAssignmentsBySession(selectedSession.Id);

                        var allOptions = new List<object>();
                        allOptions.AddRange(forums);
                        allOptions.AddRange(materials);
                        allOptions.AddRange(assignments);

                        if (allOptions.Count > 0)
                        {
                            Console.WriteLine("\nOptions:");
                            int optionNumber = 1;
                            foreach (var currentOption in allOptions)
                            {
                                Console.WriteLine($"{optionNumber}. {GetOptionTitle(currentOption)}");
                                optionNumber++;
                            }

                            Console.Write("Choose an option: ");
                            int selectedOption;
                            if (int.TryParse(Console.ReadLine(), out selectedOption) && selectedOption > 0 && selectedOption <= allOptions.Count)
                            {
                                HandleSelectedOption(allOptions[selectedOption - 1], selectedSession, materials, selectedOption, learningId);
                            }
                            else
                            {
                                Console.WriteLine("Invalid option. Returning to the main menu.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No options available for the selected session.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid Session option. Returning to the main menu.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Returning to the main menu.");
                }
            }
        }
        else
        {
            Console.WriteLine("\tNo sessions found for the selected Learning item.\n");
        }
    }

    private string GetOptionTitle(object option)
    {
        if (option is Forum forum)
        {
            return $"Forum: {forum.ForumTitle}";
        }
        else if (option is Material material)
        {
            return $"Material: {material.MaterialTitle}";
        }
        else if (option is Assignment assignment)
        {
            return $"Assignment: {assignment.AssignmentTitle}";
        }

        return string.Empty;
    }

    private void HandleSelectedOption(object option, Session selectedSession, List<Material> materials, int selectedOption, int learningId)
    {
        if (option is Forum forum)
        {
            ViewForum(selectedSession.Id, learningId);
        }
        else if (option is Material material)
        {
            int materialIndex = materials.IndexOf(material);
            ViewMaterial(materials, materialIndex, learningId);
        }
        else if (option is Assignment assignment)
        {
            ViewAssignment(selectedSession.Id, assignment.Id, learningId);
        }
    }

    //private void ViewMaterial(List<Material> materials, int selectedMaterialIndex, int learningId)
    //{
    //    if (materials != null && selectedMaterialIndex >= 0 && selectedMaterialIndex < materials.Count)
    //    {
    //        var selectedMaterial = materials[selectedMaterialIndex];

    //        Console.WriteLine($"You selected Material: {selectedMaterial.MaterialTitle}");
    //        Console.WriteLine($"Content: {selectedMaterial.MaterialContent}");
    //        Console.WriteLine("----------------------------");
    //    }
    //    else
    //    {
    //        Console.WriteLine("Invalid material selection or no materials found for this session.");
    //    }
    //    ShowSessions(learningId);
    //}

    private void ViewMaterial(List<Material> materials, int selectedMaterialIndex, int learningId)
    {
        if (materials != null && selectedMaterialIndex >= 0 && selectedMaterialIndex < materials.Count)
        {
            var selectedMaterial = materials[selectedMaterialIndex];

            Console.WriteLine($"\n Material Title: {selectedMaterial.MaterialTitle}");
            Console.WriteLine($"Material Content: {selectedMaterial.MaterialContent}");

            var materialDetails = materialDtlService.GetMaterialDetailsByMaterialId(selectedMaterial.Id);

            if (materialDetails != null && materialDetails.Count > 0)
            {
                Console.WriteLine($"\nMaterial File for {selectedMaterial.MaterialTitle}:");

                foreach (var materialDetail in materialDetails)
                {
                    if (materialDetail.MaterialFile != null)
                    {
                        Console.WriteLine($"- Material File: {materialDetail.MaterialFile.FileTitle}{materialDetail.MaterialFile.FileExtension}");
                    }
                    else
                    {
                        Console.WriteLine("There are no material files in this material");
                    }
                }
            }
            else
            {
                Console.WriteLine("No material details found for this material.");
            }
        }
        else
        {
            Console.WriteLine("Invalid material selection or no materials found for this session.");
        }

        ShowSessions(learningId);
    }



    private void ViewForum(int sessionId, int learningId)
    {
        var forums = forumService.GetForum(sessionId);

        if (forums != null && forums.Count > 0)
        {
            Console.WriteLine($"\n--[Forum]--");

            foreach (var forum in forums)
            {
                Console.WriteLine($" Lecturer: {forum.Lecturer.Fullname}");
                Console.WriteLine($" Forum Title:  {forum.ForumTitle}");
                Console.WriteLine($" Forum Content:  {forum.ForumContent}");

                var forumDetails = forumService.GetForumDetailsByForumId(forum.Id);

                foreach (var forumDetail in forumDetails)
                {
                    Console.WriteLine($"  {forumDetail.User.Fullname} :  {forumDetail.ForumDtlContent}");
                }

                bool addAnotherComment = true;

                while (addAnotherComment)
                {
                    Console.Write("Add your comment (or type '0' to go back): ");
                    string forumContent = Console.ReadLine();

                    if (forumContent == "0")
                    {
                        addAnotherComment = false;
                    }
                    else
                    {
                        forumService.AddContentToForumDtl(forum.Id, forumContent);

                        var updatedForumDetails = forumService.GetForumDetailsByForumId(forum.Id);

                        foreach (var updatedForumDetail in updatedForumDetails)
                        {
                            Console.WriteLine($"  {updatedForumDetail.User.Fullname} :  {updatedForumDetail.ForumDtlContent}");
                        }
                    }
                }

                Console.WriteLine("----------------------------------------");
            }
        }
        else
        {
            Console.WriteLine("No forum posts found for this session.");
        }
        ShowSessions(learningId);
    }

    private void ViewAssignment(int sessionId, int assignmentId, int learningId)
    {
        var assignment = assignmentService.GetAssignmentById(assignmentId);

        if (assignment != null)
        {
            // cek apakah sudah ada sudah submit assgnmnt ini oleh student ini
            bool isAlreadySubmitted = submissionService.HasSubmission(assignment.Id, loggedInUser.Id);
            if (isAlreadySubmitted)
            {
                Console.WriteLine("\nYou have already submitted your answers for this assignment.");

                ShowSessions(learningId);
            }

            //DateTime startTime = DateTime.Now;

            var submission = new Submission
            {
                AssignmentId = assignment.Id,
                StudentId = loggedInUser.Id,
                SubmissionGrade = 0.0, // Grade update later
            };

            int submissionId = submissionService.AddSubmission(submission);

            Console.WriteLine($"\nTitle: {assignment.AssignmentTitle}");
            Console.WriteLine($"Duration: {assignment.AssignmentDuration} minutes");
            Console.WriteLine("--------------------------------------------");

            var questions = assignmentService.GetAssignmentDtl(assignment.Id);

            if (questions == null || !questions.Any())
            {
                Console.WriteLine("No questions in this assignment.");
                ShowSessions(learningId);
            }

            List<SubmissionDtl> submissionDetails = new List<SubmissionDtl>();
            int questionNumber = 1;
            foreach (var question in questions)
            {
                //if (DateTime.Now > startTime.AddMinutes(assignment.AssignmentDuration))
                //{
                //    Console.WriteLine("The assignment time has expired!");
                //    break;
                //}

                Console.WriteLine($"Question {questionNumber}: {question.Question.QuestionContent}");
                questionNumber++;

                var selectedQuestion = assignmentService.GetQuestionById(question.Id);

                if (question.Question.QuestionType == QuestionType.QuestionMultipleChoice)
                {
                    var choices = assignmentService.GetQuestionChoicesByQuestionId(question.Id);
                    for (int i = 0; i < choices.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {choices[i].OptionAbc}. {choices[i].OptionContent}");
                    }

                    Console.Write("Choose an answer (enter 0 to skip): ");
                    int userChoice;
                    while (!int.TryParse(Console.ReadLine(), out userChoice) || userChoice < 0 || userChoice > choices.Count)
                    {
                        if (userChoice == 0)
                        {
                            break;
                        }
                        Console.WriteLine("Invalid input. Please enter a number between 0 and " + choices.Count);
                        Console.Write("Choose an answer (enter 0 to skip): ");
                    }

                    if (userChoice == 0)
                    {
                        Console.WriteLine("----------------------------------------");
                        continue;
                    }

                    var selectedChoice = choices[userChoice - 1];

                    var answer = new SubmissionDtl()
                    {
                        SubmissionId = submissionId,
                        QuestionId = selectedQuestion.Id,
                        SubmissionChoiceId = selectedChoice.Id,
                    };
                    submissionDetails.Add(answer);
                }
                else if (question.Question.QuestionType == QuestionType.QuestionFile)
                {
                    Console.Write("How many files? ");
                    int fileCount;
                    while (!int.TryParse(Console.ReadLine(), out fileCount) || fileCount < 0)
                    {
                        Console.WriteLine("Invalid input.");
                        Console.Write("How many files? ");
                    }

                    for (int i = 0; i < fileCount; i++)
                    {
                        Console.Write($"Enter file title {i + 1}: ");
                        string fileTitle = Console.ReadLine();
                        Console.Write($"Enter file extension {i + 1}: ");
                        string fileExtension = Console.ReadLine();

                        FileLms newFile = new FileLms
                        {
                            FileTitle = fileTitle,
                            FileExtension = fileExtension
                        };

                        int fileId = fileService.CreateFile(fileTitle, fileExtension);

                        var answer = new SubmissionDtlFile()
                        {
                            SubmissionId = submissionId,
                            SubmissionFileId = fileId,
                        };
                        submissionService.AddSubmissionDtlFile(answer);
                    }
                }
                else
                {
                    Console.Write("Enter your answer (press Enter to skip): ");
                    string userAnswer = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(userAnswer))
                    {
                        Console.WriteLine("----------------------------------------");
                        continue;
                    }

                    var answer = new SubmissionDtl()
                    {
                        SubmissionChoiceId = null,
                        SubmissionId = submissionId,
                        QuestionId = selectedQuestion.Id,
                        SubmissionContent = userAnswer,
                    };

                    submissionDetails.Add(answer);
                }
                Console.WriteLine("----------------------------------------");
            }

            if (submissionDetails.Count > 0)
            {
                foreach (var answer in submissionDetails)
                {
                    submissionService.AddSubmissionDtl(answer);
                }

                Console.WriteLine("--- Your answers have been submitted! ---");
            }
        }
        else
        {
            Console.WriteLine("Assignment not found.");
        }

        ShowSessions(learningId);
    }
}
