using LearningManagement.DBConfig;
using LearningManagement.Model;

namespace LearningManagement.IService
{
    public interface ISessionService
    {
        Attendance? GetSessionAttendanceStatus(int sessionId);
        Attendance RequestAttendance(int sessionId);
        int AddSession(Session newSession, int CreatedBy);
        List<Session> GetSessionsByLecturer(int lecturerId);
        List<Session> GetSessionsByLearning(int learningId);
    }
}
