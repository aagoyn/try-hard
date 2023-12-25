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
    public class MaterialService : IMaterialService
    {
        private readonly IMaterialRepo materialRepo;
        private readonly SessionHelper sessionHelper;
        private readonly DBContextConfig context;

        public MaterialService(IMaterialRepo materialRepo, SessionHelper sessionHelper)
        {
            context = new DBContextConfig();
            this.materialRepo = materialRepo;
            this.sessionHelper = sessionHelper;
        }
        public int AddMaterial(Material newMaterial)
        {
            newMaterial.CreatedBy = sessionHelper.UserId;
            newMaterial.CreatedAt = DateTime.Now;
            newMaterial.IsActive = true;

            return materialRepo.AddMaterial(newMaterial, context);
        }

        public List<Material> GetMaterialBySession(int sessionId)
        {
            return materialRepo.GetMaterialBySession(sessionId);
        }

    }
}
