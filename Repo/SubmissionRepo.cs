using LearningManagement.DBConfig;
using LearningManagement.Model;
using LearningManagement.Helper;
using LearningManagement.IRepo;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.EntityFrameworkCore;


namespace LearningManagement.Repo
{
    public class SubmissionRepo : ISubmissionRepo
    {

        public int AddSubmissionDtl(SubmissionDtl submissionDtl, DBContextConfig context)
        {

            context.SubmissionDtls.Add(submissionDtl);
            context.SaveChanges();

            return submissionDtl.Id;
        }

        public int AddSubmission(Submission submission, DBContextConfig context)
        {
            context.Submissions.Add(submission);
            context.SaveChanges();

            return submission.Id;
        }

        public void AddSubmissionDtlFile(SubmissionDtlFile submissionDtlFile, DBContextConfig context)
        {
            context.SubmissionDtlFiles.Add(submissionDtlFile);
            context.SaveChanges();
        }

        public bool HasSubmission(int assignmentId, int studentId, DBContextConfig context)
        {
            return context.Submissions.Any(s => s.AssignmentId == assignmentId && s.StudentId == studentId);
        }


        //public int SaveAnswers(List<QuestionAnswer> answers, int assignmentId, int studentId, int CreatedBy, DateTime CreatedAt, bool IsActive, DBContextConfig context)
        //{
        //    var submission = new Submission
        //    {
        //        AssignmentId = assignmentId,
        //        StudentId = studentId,
        //        SubmissionGrade = 0.0,
        //        CreatedBy = CreatedBy,
        //        CreatedAt = CreatedAt,
        //        IsActive = IsActive,
        //    };
        //    context.Submissions.Add(submission);
        //    context.SaveChanges();
        //    //return submission.Id;

        //    foreach (var answer in answers)
        //    {
        //        var submissionDetail = new SubmissionDtl
        //        {
        //            SubmissionId = submission.Id,
        //            SubmissionContent = answer.IsFile ? null : answer.TextAnswer,
        //            SubmissionChoiceId = answer.IsChoice && answer.ChoiceId.HasValue ? answer.ChoiceId.Value : 0,
        //            CreatedBy = CreatedBy,
        //            CreatedAt = CreatedAt,
        //            IsActive = IsActive,
        //        };
        //        context.SubmissionDtls.Add(submissionDetail);

        //        if (answer.IsFile)
        //        {
        //            var fileEntry = new FileLms
        //            {
        //                FileTitle = answer.FileTitle,
        //                FileExtension = answer.FileExtension,
        //                CreatedBy = CreatedBy,
        //                CreatedAt = CreatedAt,
        //                IsActive = IsActive,
        //            };
        //            context.LmsFiles.Add(fileEntry);
        //            context.SaveChanges();  // Tambahkan perubahan ke database di sini

        //            var submissionDtlFile = new SubmissionDtlFile
        //            {
        //                SubmissionDtlId = submissionDetail.Id,
        //                SubmissionFileId = fileEntry.Id,
        //                CreatedBy = CreatedBy,
        //                CreatedAt = CreatedAt,
        //                IsActive = IsActive,
        //            };
        //            context.SubmissionDtlFiles.Add(submissionDtlFile);
        //        }
        //    }
        //    context.SaveChanges();  // Pindahkan perubahan ke database di sini


        //    return submission.Id;
        //}
    }

}
