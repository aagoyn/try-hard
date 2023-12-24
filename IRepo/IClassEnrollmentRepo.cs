using LearningManagement.Model;
using LearningManagement.DBConfig;

namespace LearningManagement.IRepo;

public interface IClassEnrollmentRepo
{
    int EnrollStudent(ClassEnrollment enrollment, DBContextConfig context);
    List<Class> GetUnenrolledClasses(int studentId, DBContextConfig context);

    List<Class> GetEnrolledClasses(int studentId, DBContextConfig context);
}
