using LearningManagement.DBConfig;
using LearningManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagement.IRepo
{
    public interface ILearningRepo
    {
        void AddLearning(int classId, string day, DateOnly learningDate, int CreatedBy, DBContextConfig context);
        List<Learning> GetLearningByLecturer(int lecturerId, DBContextConfig context);
        List<Learning> GetLearningByClass(int classId, DBContextConfig context);
    }
}
