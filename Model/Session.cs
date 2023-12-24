using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearningManagement.Model;

[Table("t_session")]
[Index(nameof(SessionStart), nameof(SessionEnd), IsUnique = true, Name = "session_time_ck")]
public class Session : BaseModel
{

    [Column("session_title"), MaxLength(50)]
    public string SessionTitle { get; set; }

    [Column("session_start")]
    public TimeSpan SessionStart { get; set; }

    [Column("session_end")]
    public TimeSpan SessionEnd { get; set; }

    [Column("learning_id")]
    public int LearningId { get; set; }

    [ForeignKey(nameof(LearningId))]
    public Learning Learning { get; set; }
}
