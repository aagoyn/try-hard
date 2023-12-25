using System.ComponentModel.DataAnnotations.Schema;

namespace LearningManagement.Model;

[Table("t_submission_dtl")]
public class SubmissionDtl : BaseModel
{
    [Column("submission_id")]
    public int SubmissionId { get; set; }

    [ForeignKey(nameof(SubmissionId))]
    public Submission Submission { get; set; }

    [Column("question_id")]
    public int QuestionId { get; set; }

    [ForeignKey(nameof(QuestionId))]
    public Question Question { get; set; }

    [Column("submission_content", TypeName = "TEXT")]
    public string? SubmissionContent { get; set; }

    [Column("submission_choice")]
    public int? SubmissionChoiceId { get; set; }

    [ForeignKey(nameof(SubmissionChoiceId))]
    public QuestionChoice? SubmissionChoice { get; set; }
}
