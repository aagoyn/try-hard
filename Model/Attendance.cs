using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearningManagement.Model;

[Table("t_attendance")]
[Index(nameof(StudentId), nameof(SessionId), IsUnique = true, Name = "attendance_ck")]
public class Attendance : BaseModel
{
    [Column("student_id")]
    public int StudentId { get; set; }

    [ForeignKey(nameof(StudentId))]
    public User Student { get; set; }

    [Column("session_id")]
    public int SessionId { get; set; }

    [ForeignKey(nameof(SessionId))]
    public Session Session { get; set; }

    [Column("is_approve")]
    public bool IsApprove { get; set; }
}
