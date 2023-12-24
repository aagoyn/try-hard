using LearningManagement.Model;

namespace LearningManagement.IService;

public interface IClassService
{
    int AddClass(Class newClass, int LecturerId);
    void AssignLecturerToClass(int lecturerId, int classId);
    List<Class> GetClassesAssignedToLecturer(int lecturerId);
    Class GetClassById(int classId);
    int EnrollStudent(int classId, int studentId);
    List<Class> GetUnenrolledClasses(int studentId);
    List<Class> GetEnrolledClasses(int studentId);


}
