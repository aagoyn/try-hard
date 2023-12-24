using System.ComponentModel.DataAnnotations.Schema;

namespace LearningManagement.Model;

[Table("t_submission_dtl_file")]
public class SubmissionDtlFile : BaseModel
{
    [Column("submission_dtl_id")]
    public int SubmissionDtlId { get; set; }

    [ForeignKey(nameof(SubmissionDtlId))]
    public SubmissionDtl SubmissionDtl { get; set; }

    [Column("submission_file")]
    public int SubmissionFileId { get; set; }

    [ForeignKey(nameof(SubmissionFileId))]
    public FileLms SubmissionFile { get; set; }
}
