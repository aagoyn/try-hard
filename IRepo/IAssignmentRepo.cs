using LearningManagement.DBConfig;
using LearningManagement.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagement.IRepo
{
    public interface IAssignmentRepo
    {
        int AddAssignment(Assignment newAssignment, DBContextConfig context);
        Assignment GetAssignmentById(int assignmentId, DBContextConfig context);
        List<Assignment> GetAssignmentsBySession(int sessionId, DBContextConfig context);
    }
}
