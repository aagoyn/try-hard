
using System.ComponentModel.DataAnnotations.Schema;

namespace LearningManagement.Model;

[Table("t_submission")]
public class Submission : BaseModel
{
    [Column("assignment_id")]
    public int AssignmentId { get; set; }

    [ForeignKey(nameof(AssignmentId))]
    public Assignment Assignment { get; set; }

    [Column("student_id")]
    public int StudentId { get; set; }

    [ForeignKey(nameof(StudentId))]
    public User Student { get; set; }

    [Column("submission_grade")]
    public double SubmissionGrade { get; set; }
}
