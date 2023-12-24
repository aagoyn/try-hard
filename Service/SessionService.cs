using LearningManagement.DBConfig;
using LearningManagement.Helper;
using LearningManagement.IRepo;
using LearningManagement.IService;
using LearningManagement.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagement.Service
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepo sessionRepo;
        private readonly SessionHelper sessionHelper;
        private readonly DBContextConfig context;

        public SessionService(ISessionRepo sessionRepo, SessionHelper sessionHelper)
        {
            context = new DBContextConfig();
            this.sessionRepo = sessionRepo;
            this.sessionHelper = sessionHelper;
        }

        public Attendance? GetSessionAttendanceStatus(int sessionId)
        {
            return sessionRepo.GetSessionAttendanceStatus(sessionId, sessionHelper.UserId, context);
        }

        public Attendance RequestAttendance(int sessionId)
        {
            var attendance = new Attendance()
            {
                StudentId = sessionHelper.UserId,
                SessionId = sessionId,
                IsApprove = false,
                CreatedAt = DateTime.Now,
            };
            return sessionRepo.RequestAttendance(attendance, context);
        }

        public int AddSession(Session newSession, int CreatedBy)
        {
            return sessionRepo.AddSession(newSession, CreatedBy);
        }

        public List<Session> GetSessionsByLecturer(int lecturerId)
        {
            return sessionRepo.GetSessionsByLecturer(lecturerId);
        }

        public List<Session> GetSessionsByLearning(int learningId)
        {
            return sessionRepo.GetSessionsByLearning(learningId);
        }
    }
}
