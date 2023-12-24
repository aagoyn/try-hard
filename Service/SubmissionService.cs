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
        return submissionRepo.AddSubmissionDtl(submissionDtl, context);
    }

    public int AddSubmission(Submission submission)
    {
        return submissionRepo.AddSubmission(submission, context);
    }

    public void AddSubmissionDtlFile(SubmissionDtlFile submissionDtlFile)
    {
        submissionRepo.AddSubmissionDtlFile(submissionDtlFile, context);
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
