using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearningManagement.Model;

[Table("t_forum")]
[Index(nameof(SessionId), IsUnique = true, Name = "session_is_unique")]
public class Forum : BaseModel
{
    [Column("forum_title")]
    public string ForumTitle { get; set; }

    [Column("forum_content")]
    public string ForumContent { get; set; }

    [Column("session_id")]
    public int SessionId { get; set; }

    [ForeignKey(nameof(SessionId))]
    public Session Session { get; set; }

    [Column("lecturer_id")]
    public int LecturerId { get; set; }

    [ForeignKey(nameof(LecturerId))]
    public User Lecturer { get; set; }
}
