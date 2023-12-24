using LearningManagement.DBConfig;
using LearningManagement.Model;

namespace LearningManagement.IRepo;

public interface IFileRepo
{
    int CreateFile(FileLms file, DBContextConfig context);
}
