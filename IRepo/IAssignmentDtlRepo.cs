using LearningManagement.DBConfig;
using LearningManagement.Model;

namespace LearningManagement.IRepo
{
    public interface IAssignmentDtlRepo
    {
        List<AssignmentDtl> GetAssignmentDtl(int assignmentId, DBContextConfig context);
    }
}
