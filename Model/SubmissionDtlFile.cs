using System.ComponentModel.DataAnnotations.Schema;

namespace LearningManagement.Model;

[Table("t_submission_dtl_file")]
public class SubmissionDtlFile : BaseModel
{
    [Column("submission_id")]
    public int SubmissionId { get; set; }

    [ForeignKey(nameof(SubmissionId))]
    public Submission Submission { get; set; }

    [Column("submission_file")]
    public int SubmissionFileId { get; set; }

    [ForeignKey(nameof(SubmissionFileId))]
    public FileLms SubmissionFile { get; set; }
}
