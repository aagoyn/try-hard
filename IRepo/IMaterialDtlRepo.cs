using LearningManagement.DBConfig;
using LearningManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagement.IRepo
{
    public interface IMaterialDtlRepo
    {
        void AddMaterialDetails(MaterialDtl materialDtl, DBContextConfig context);
    }
}
