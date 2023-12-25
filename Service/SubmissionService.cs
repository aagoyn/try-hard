using LearningManagement.DBConfig;
using LearningManagement.Helper;
using LearningManagement.IRepo;
using LearningManagement.IService;
using LearningManagement.Model;

namespace LearningManagement.Service;

public class SubmissionService : ISubmissionService
{
    private readonly DBContextConfig context;
    private readonly SessionHelper sessionHelper;
    private readonly ISubmissionRepo submissionRepo;

    public SubmissionService(SessionHelper sessionHelper, ISubmissionRepo submissionRepo)
    {
        context = new DBContextConfig();
        this.sessionHelper = sessionHelper;
        this.submissionRepo = submissionRepo;
    }

    public int AddSubmissionDtl(SubmissionDtl submissionDtl)
    {
        submissionDtl.CreatedBy = sessionHelper.UserId;
        submissionDtl.CreatedAt = DateTime.Now;
        submissionDtl.IsActive = true;

        return submissionRepo.AddSubmissionDtl(submissionDtl, context);
    }

    public int AddSubmission(Submission submission)
    {
        submission.CreatedBy = sessionHelper.UserId;
        submission.CreatedAt = DateTime.Now;
        submission.IsActive = true;
        return submissionRepo.AddSubmission(submission, context);
    }

    public void AddSubmissionDtlFile(SubmissionDtlFile submissionDtlFile)
    {
        submissionDtlFile.CreatedBy = sessionHelper.UserId;
        submissionDtlFile.CreatedAt = DateTime.Now;
        submissionDtlFile.IsActive = true;
        submissionRepo.AddSubmissionDtlFile(submissionDtlFile, context);
    }

    public bool HasSubmission(int assignmentId, int studentId)
    {
        return submissionRepo.HasSubmission(assignmentId, studentId, context);
    }

    //public int SaveAnswers(List<QuestionAnswer> answers, int assignmentId)
    //{
    //    int CreatedBy = sessionHelper.UserId;
    //    DateTime CreatedAt = DateTime.Now;
    //    bool IsActive = true;
    //    int studentId = sessionHelper.UserId;

    //    return submissionRepo.SaveAnswers(answers, assignmentId, studentId, CreatedBy, CreatedAt, IsActive, context);
    //}
}
