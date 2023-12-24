using LearningManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagement.IService
{
    public interface ILearningService
    {
        void AddLearning(int classId, string day, DateOnly learningDate, int CreatedBy);
        List<Learning> GetLearningByLecturer(int lecturerId);
        List<Learning> GetLearningByClass(int classId);
    }
}
