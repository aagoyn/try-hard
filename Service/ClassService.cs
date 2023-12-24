using LearningManagement.IService;
using LearningManagement.IRepo;
using LearningManagement.Model;
using LearningManagement.DBConfig;
using LearningManagement.Helper;

namespace LearningManagement.Service
{
    public class ClassService : IClassService
    {
        private readonly DBContextConfig context;
        private readonly SessionHelper sessionHelper;
        private readonly IClassRepo classRepo;
        private readonly IClassEnrollmentRepo classEnrollmentRepo;


        public ClassService(IClassRepo classRepo, IClassEnrollmentRepo classEnrollmentRepo, SessionHelper sessionHelper)
        {
            context = new DBContextConfig();
            this.classRepo = classRepo;
            this.classEnrollmentRepo = classEnrollmentRepo;
            this.sessionHelper = sessionHelper;
        }

        public int AddClass(Class newClass, int LecturerId)
        {
            newClass.CreatedAt = DateTime.Now;
            newClass.IsActive = true;
            newClass.CreatedBy = sessionHelper.UserId;

            return classRepo.AddClass(newClass, LecturerId, context);
        }

        public void AssignLecturerToClass(int lecturerId, int classId)
        {
            classRepo.AssignLecturerToClass(lecturerId, classId, context);
        }

        public List<Class> GetClassesAssignedToLecturer(int lecturerId)
        {
            return classRepo.GetClassesAssignedToLecturer(lecturerId, context);
        }

        public Class GetClassById(int classId)
        {
            return classRepo.GetClassById(classId, context);
        }

        public int EnrollStudent(int classId, int studentId)
        {
            var enrollment = new ClassEnrollment
            {
                ClassId = classId,
                StudentId = studentId,
                CreatedBy = sessionHelper.UserId,
                CreatedAt = DateTime.Now,
                IsActive = true,
            };

            return classEnrollmentRepo.EnrollStudent(enrollment, context);
        }

        public List<Class> GetUnenrolledClasses(int studentId)
        {
            return classEnrollmentRepo.GetUnenrolledClasses(studentId, context);
        }

        public List<Class> GetEnrolledClasses(int studentId)
        {
            return classEnrollmentRepo.GetEnrolledClasses(studentId, context);
        }
    }
}
