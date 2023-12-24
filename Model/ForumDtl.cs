using System.ComponentModel.DataAnnotations.Schema;

namespace LearningManagement.Model;

[Table("t_forum_dtl")]
public class ForumDtl : BaseModel
{
    [Column("forum_id")]
    public int ForumId { get; set; }

    [ForeignKey(nameof(ForumId))]
    public Forum Forum { get; set; }

    [Column("forum_dtl_content", TypeName = "TEXT")]
    public string ForumDtlContent { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; }

}
