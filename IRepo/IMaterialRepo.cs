using LearningManagement.Model;

namespace LearningManagement.IRepo;

public interface IMaterialRepo
{
    int AddMaterial(Material newMaterial, int CreatedBy);
    bool IsMaterialAvailableForSession(int sessionId);
    List<Material> GetMaterialBySession(int sessionId);
}
