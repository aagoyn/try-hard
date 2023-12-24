using LearningManagement.DBConfig;
using LearningManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagement.IRepo
{
    public interface ISessionRepo
    {
        Attendance? GetSessionAttendanceStatus(int sessionId, int studentId, DBContextConfig context);
        Attendance RequestAttendance(Attendance attendance, DBContextConfig context);
        int AddSession(Session newSession, int CreatedBy);
        List<Session> GetSessionsByLecturer(int lecturerId);
        List<Session> GetSessionsByLearning(int learningId);
    }
}
