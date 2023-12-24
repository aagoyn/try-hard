using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearningManagement.Model;

[Table("t_assignment")]
public class Assignment : BaseModel
{
    [Column("assignment_title"), MaxLength(100)]
    public string AssignmentTitle { get; set; }

    [Column("assignment_duration")]
    public int AssignmentDuration { get; set; }

    [Column("session_id")]
    public int SessionId { get; set; }

    [ForeignKey(nameof(SessionId))]
    public Session Session { get; set; }
}
