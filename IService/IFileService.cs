using LearningManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagement.IService
{
    public interface IFileService
    {
        int CreateFile(string fileTitle, string fileExtension);
    }
}
