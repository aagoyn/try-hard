using LearningManagement.DBConfig;
using LearningManagement.IRepo;
using LearningManagement.IService;
using LearningManagement.Model;
using LearningManagement.Repo;
namespace LearningManagement.Service;


public class AssignmentService : IAssignmentService
{
    public readonly DBContextConfig context;
    private readonly IAssignmentRepo assignmentRepo;
    private readonly IAssignmentDtlRepo assignmentDtlRepo;
    private readonly IQuestionRepo questionRepo;

    public AssignmentService(IAssignmentRepo assignmentRepo, IAssignmentDtlRepo assignmentDtlRepo, IQuestionRepo questionRepo)
    {
        context = new DBContextConfig();
        this.assignmentRepo = assignmentRepo;
        this.assignmentDtlRepo = assignmentDtlRepo;
        this.questionRepo = questionRepo;
    }

    public Assignment GetAssignmentById(int assignmentId)
    {
        return assignmentRepo.GetAssignmentById(assignmentId, context);
    }


    public List<Assignment> GetAssignmentsBySession(int sessionId)
    {
        return assignmentRepo.GetAssignmentsBySession(sessionId, context);
    }

    public List<AssignmentDtl> GetAssignmentDtl(int assignmentId)
    {
        return assignmentDtlRepo.GetAssignmentDtl(assignmentId, context);
    }

    public List<Question> ShowQuestions()
    {
        return questionRepo.ShowQuestions(context);
    }
    public List<QuestionChoice> GetQuestionChoicesByQuestionId(int questionId)
    {
        return questionRepo.GetQuestionChoicesByQuestionId(questionId, context);
    }

    public QuestionFile? GetQuestionFileByQuestionId(int questionId)
    {
        return questionRepo.GetQuestionFileByQuestionId(questionId, context);
    }

    public Question GetQuestionById(int questionId)
    {
        return questionRepo.GetQuestionById(questionId, context);
    }

    public int AddAssignment(Assignment newAssignment)
    {
        newAssignment.CreatedAt = DateTime.Now;
        newAssignment.IsActive = true;
        return assignmentRepo.AddAssignment(newAssignment, context);
    }
}
