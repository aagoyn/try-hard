using LearningManagement.DBConfig;
using LearningManagement.IRepo;
using LearningManagement.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagement.Repo
{
    public class AssignmentDtlRepo : IAssignmentDtlRepo
    {
        public List<AssignmentDtl> GetAssignmentDtl(int assignmentId, DBContextConfig context)
        {
            var assignmentDetails = context.AssignmentDtls
                .Include(x => x.Question)
                .Where(a => a.AssignmentId == assignmentId)
                .ToList();

            return assignmentDetails;
        }

    }
}
