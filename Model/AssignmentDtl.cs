using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearningManagement.Model;

[Table("t_assignment_dtl")]
[Index(nameof(QuestionId), nameof(AssignmentId), IsUnique = true, Name = "assignmnet_dtl_ck")]
public class AssignmentDtl : BaseModel
{
    [Column("question_id")]
    public int QuestionId { get; set; }

    [ForeignKey(nameof(QuestionId))]
    public Question Question { get; set; }

    [Column("assignment_id")]
    public int AssignmentId { get; set; }

    [ForeignKey(nameof(AssignmentId))]
    public Assignment Assignment { get; set; }
}
