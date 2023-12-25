using LearningManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagement.IService
{
    public interface IMaterialService
    {
        int AddMaterial(Material newMaterial);
        List<Material> GetMaterialBySession(int sessionId);

    }
}
