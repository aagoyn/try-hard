using LearningManagement.DBConfig;
using LearningManagement.Helper;
using LearningManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagement.IRepo
{
    public interface ISubmissionRepo
    {
        int AddSubmissionDtl(SubmissionDtl submissionDtl, DBContextConfig context);
        int AddSubmission(Submission submission, DBContextConfig context);
        void AddSubmissionDtlFile(SubmissionDtlFile submissionDtlFile, DBContextConfig context);
        bool HasSubmission(int assignmentId, int studentId, DBContextConfig context);
    }
}
