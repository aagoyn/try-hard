using LearningManagement.DBConfig;
using LearningManagement.Model;

namespace LearningManagement.IRepo;

public interface IClassRepo
{
    int AddClass(Class newClass, int LecturerId, DBContextConfig context);
    void AssignLecturerToClass(int lecturerId, int classId, DBContextConfig context);
    List<Class> GetClassesAssignedToLecturer(int lecturerId, DBContextConfig context);
    Class GetClassById(int classId, DBContextConfig context);
}
