using LearningManagement.DBConfig;
using LearningManagement.Model;


namespace LearningManagement.IService
{
    public interface IMaterialDtlService
    {
        void AddMaterialDetails(MaterialDtl materialDtl);
        List<MaterialDtl> GetMaterialDetailsByMaterialId(int materialId);
    }
}
