using LearningManagement.DBConfig;
using LearningManagement.Helper;
using LearningManagement.IRepo;
using LearningManagement.IService;
using LearningManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagement.Service
{
    public class MaterialDtlService : IMaterialDtlService
    {
        private readonly IMaterialDtlRepo materialDtlRepo;
        private readonly SessionHelper sessionHelper;
        private readonly DBContextConfig context;
        public MaterialDtlService(IMaterialDtlRepo materialDtlRepo, SessionHelper sessionHelper)
        {
            context = new DBContextConfig();
            this.materialDtlRepo = materialDtlRepo;
            this.sessionHelper = sessionHelper;
        }
        public void AddMaterialDetails(MaterialDtl materialDtl)
        {
            materialDtl.CreatedBy = sessionHelper.UserId;
            materialDtl.CreatedAt = DateTime.Now;
            materialDtl.IsActive = true;

            materialDtlRepo.AddMaterialDetails(materialDtl, context);
        }

        public List<MaterialDtl> GetMaterialDetailsByMaterialId(int materialId)
        {
            return materialDtlRepo.GetMaterialDetailsByMaterialId(materialId, context);
        }
    }
}
