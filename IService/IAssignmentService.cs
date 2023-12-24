using LearningManagement.DBConfig;
using LearningManagement.Model;

namespace LearningManagement.IService
{
    public interface IAssignmentService
    {
        int AddAssignment(Assignment newAssignment);
        List<Assignment> GetAssignmentsBySession(int sessionId);
        List<AssignmentDtl> GetAssignmentDtl(int assignmentId);
        List<Question> ShowQuestions();
        List<QuestionChoice> GetQuestionChoicesByQuestionId(int questionId);
        QuestionFile? GetQuestionFileByQuestionId(int questionId);
        Assignment GetAssignmentById(int assignmentId);
        Question GetQuestionById(int questionId);
    }
}
