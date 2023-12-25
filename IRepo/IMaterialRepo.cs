using LearningManagement.DBConfig;
using LearningManagement.Model;

namespace LearningManagement.IRepo;

public interface IMaterialRepo
{
    int AddMaterial(Material newMaterial, DBContextConfig context);
    List<Material> GetMaterialBySession(int sessionId);
}
