using LearningManagement.DBConfig;
using LearningManagement.IRepo;
using LearningManagement.IService;
using LearningManagement.Model;

namespace LearningManagement.Service
{
    public class LearningService : ILearningService
    {
        private readonly ILearningRepo learningRepo;
        private readonly DBContextConfig context;

        public LearningService(ILearningRepo learningRepo)
        {
            context = new DBContextConfig();
            this.learningRepo = learningRepo;
        }
        public void AddLearning(int classId, string day, DateOnly learningDate, int CreatedBy)
        {
            learningRepo.AddLearning(classId, day, learningDate, CreatedBy, context);
        }
        public List<Learning> GetLearningByLecturer(int lecturerId)
        {
            return learningRepo.GetLearningByLecturer(lecturerId, context);
        }

        public List<Learning> GetLearningByClass(int classId)
        {
            return learningRepo.GetLearningByClass(classId, context);
        }
    }
}
