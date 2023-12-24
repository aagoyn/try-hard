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

        public MaterialService(IMaterialRepo materialRepo)
        {
            this.materialRepo = materialRepo;
        }
        public int AddMaterial(Material newMaterial, int CreatedBy)
        {
            return materialRepo.AddMaterial(newMaterial, CreatedBy);
        }

        public bool IsMaterialAvailableForSession(int sessionId)
        {
            return materialRepo.IsMaterialAvailableForSession(sessionId);
        }
        public List<Material> GetMaterialBySession(int sessionId)
        {
            return materialRepo.GetMaterialBySession(sessionId);
        }

    }
}
