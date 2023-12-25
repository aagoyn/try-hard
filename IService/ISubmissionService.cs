using LearningManagement.DBConfig;
using LearningManagement.Helper;
using LearningManagement.Model;

namespace LearningManagement.IService;

public interface ISubmissionService
{
    int AddSubmission(Submission submission);
    int AddSubmissionDtl(SubmissionDtl submissionDtl);
    void AddSubmissionDtlFile(SubmissionDtlFile submissionDtlFile);
    bool HasSubmission(int assignmentId, int studentId);
}
